using Emporium.Application.Configuration.Services;
using Emporium.Domain.Providers;
using Emporium.Domain.SeedWork;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace Emporium.Infrastructure.CosmosDB;

public class UnitOfWork : IUnitOfWork
{
    private readonly IContainerFactory _containerFactory;
    private readonly List<(IBaseEntity<TypedIdValueBase> entity, string containerName, EntityState state, PartitionKey partitionKey)> _trackedChanges = new();

    // Logger opcional
    // private readonly ILogger<CosmosUnitOfWork> _logger;
    // public CosmosUnitOfWork(IContainerFactory containerFactory, ILogger<CosmosUnitOfWork> logger)
    public UnitOfWork(IContainerFactory containerFactory)
    {
        _containerFactory = containerFactory ?? throw new ArgumentNullException(nameof(containerFactory));
        // _logger = logger;
    }

    // Método interno llamado por los repositorios para registrar cambios
    internal void RegisterChange(IBaseEntity<TypedIdValueBase> entity, string containerName, EntityState state, PartitionKey partitionKey)
    {
        // Validaciones básicas
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));
        //if (string.IsNullOrWhiteSpace(entity.Id)) throw new ArgumentException("Entity must have an Id.", nameof(entity));
        //if (string.IsNullOrWhiteSpace(entity.PartitionKey)) throw new ArgumentException("Entity must have a PartitionKey.", nameof(entity));

        // Podrías añadir lógica para evitar registrar la misma entidad múltiples veces
        // o para manejar transiciones de estado (ej: Add -> Delete se anulan).
        // Por simplicidad, solo añadimos a la lista.
        _trackedChanges.Add((entity, containerName, state, partitionKey));
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (!_trackedChanges.Any())
        {
            // _logger?.LogInformation("No changes detected in Unit of Work.");
            return true;
        }

        // Validación: Asegurar que todas las entidades son del mismo contenedor y PK
        string? targetContainerName = null;
        PartitionKey? targetPartitionKey = null;
        Container? targetContainer = null;

        try
        {
            foreach (var (entity, containerName, _, partitionKey) in _trackedChanges)
            {
                var currentPartitionKey = partitionKey;

                if (targetContainerName == null) // Primera entidad
                {
                    targetContainerName = containerName;
                    targetPartitionKey = currentPartitionKey;
                    targetContainer = _containerFactory.GetContainer(targetContainerName);
                    if (targetContainer == null)
                    {
                        throw new InvalidOperationException($"Transactional target container '{targetContainerName}' could not be retrieved.");
                    }
                }
                else // Entidades subsiguientes
                {
                    if (containerName != targetContainerName)
                    {
                        throw new InvalidOperationException(
                            $"Cross-container transaction attempt: Cannot save changes for both '{targetContainerName}' and '{containerName}' in one transaction. Use Outbox pattern.");
                    }
                    if (currentPartitionKey != targetPartitionKey)
                    {
                        throw new InvalidOperationException(
                           $"Cross-partition transaction attempt within container '{targetContainerName}': Cannot save changes for both PartitionKey '{targetPartitionKey}' and '{currentPartitionKey}' in one transaction.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // _logger?.LogError(ex, "Validation failed before creating transactional batch.");
            _trackedChanges.Clear(); // Limpiar estado si la validación falla
            throw; // Relanzar la excepción de validación
        }


        if (targetContainer == null || !targetPartitionKey.HasValue)
        {
            _trackedChanges.Clear();
            // _logger?.LogError("Target container or partition key could not be determined, clearing changes.");
            throw new InvalidOperationException("Failed to determine target container or partition key for the transaction.");
        }

        //var id = new ProviderId(Guid.NewGuid());
        //var json = JsonConvert.SerializeObject(id);  // Usa el conversor
        //var deserialized = JsonConvert.DeserializeObject<ProviderId>(json);
        //Console.WriteLine(deserialized);



        //_ = await targetContainer.CreateItemAsync<Provider>((Provider)_trackedChanges.First().entity, new PartitionKey(_trackedChanges.First().entity.Id.Value.ToString()));


        // Construir y ejecutar el batch
        var batch = targetContainer.CreateTransactionalBatch(targetPartitionKey.Value);
        //List<IEntity> entitiesWithEvents = new(); // Solo entidades que implementan IEntity

        foreach (var (entity, _, state, _) in _trackedChanges)
        {
            switch (state)
            {
                case EntityState.Created:
                    batch.CreateItem(entity);
                    break;
                case EntityState.Updated:
                    // Usar ETag para concurrencia optimista si está disponible
                    var replaceOptions = entity.Version != null ? new TransactionalBatchItemRequestOptions { IfMatchEtag = entity.Version } : null;
                    batch.ReplaceItem(entity.Id.Value.ToString(), entity, replaceOptions);
                    break;
                case EntityState.Deleted:
                    var deleteOptions = entity.Version != null ? new TransactionalBatchItemRequestOptions { IfMatchEtag = entity.Version } : null;
                    batch.DeleteItem(entity.Id.Value.ToString(), deleteOptions);
                    break;
            }

            // Si la entidad tiene eventos de dominio, añadirla a la lista para procesar
            //if (entity is IEntity domainEntity && domainEntity.DomainEvents.Any())
            //{
            //    entitiesWithEvents.Add(domainEntity);
            //}
        }

        // Crear y añadir mensajes Outbox para los eventos de dominio
        //foreach (var entityWithEvents in entitiesWithEvents)
        //{
        //    foreach (var domainEvent in entityWithEvents.DomainEvents)
        //    {
        //        var outboxMessage = new OutboxMessage(
        //           partitionKey: entityWithEvents.PartitionKey, // Misma PK
        //           occurredOnUtc: domainEvent.OccurredOn,
        //           eventType: domainEvent.GetType().FullName!,
        //           // Serializar el evento completo como JSON
        //           eventData: JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), new JsonSerializerOptions { WriteIndented = false }) // Usar el tipo real
        //       );
        //        batch.CreateItem(outboxMessage); // Añadir al mismo batch
        //    }
        //}

        // Ejecutar el batch
        // _logger?.LogInformation("Executing Transactional Batch for container '{Container}' and PartitionKey '{PartitionKey}' with {OperationCount} operations.", targetContainerName, targetPartitionKey.ToString(), batch.Operations.Count);
        using var response = await batch.ExecuteAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            // _logger?.LogInformation("Transactional Batch successful. Status Code: {StatusCode}, RU Cost: {RequestCharge}", response.StatusCode, response.RequestCharge);
            // Limpiar eventos de dominio de las entidades AHORA que se guardó
            //foreach (var entityWithEvents in entitiesWithEvents)
            //{
            //    entityWithEvents.ClearDomainEvents();
            //}
            _trackedChanges.Clear(); // Limpiar estado del UoW
            return true;
        }
        else
        {
            // El batch falló. NO limpiar eventos, NO limpiar _trackedChanges (o sí, depende de estrategia de reintento).
            // _logger?.LogError("Transactional Batch failed. Status Code: {StatusCode}, Error: {ErrorMessage}, RU Cost: {RequestCharge}", response.StatusCode, response.ErrorMessage, response.RequestCharge);
            // Opcional: Loggear detalles de cada operación fallida
            // for(int i=0; i<response.Count; i++) { if (!response[i].IsSuccessStatusCode) _logger.LogError("  Operation {Index} failed: {StatusCode}", i, response[i].StatusCode); }

            _trackedChanges.Clear(); // Limpiar para evitar reintentos accidentales con el mismo estado. Considera lanzar una excepción detallada.
            // Considera lanzar una excepción personalizada con la respuesta
            // throw new CosmosBatchOperationException("Cosmos DB batch operation failed.", response);
            return false;
        }
    }

    public async ValueTask DisposeAsync()
    {
        // Asegura que se limpien los cambios si no se llamó a SaveChangesAsync
        // o si SaveChangesAsync falló y decidimos limpiar aquí.
        _trackedChanges.Clear();
        // No hay otros recursos explícitos que liberar aquí normalmente.
        await Task.CompletedTask;
    }
}