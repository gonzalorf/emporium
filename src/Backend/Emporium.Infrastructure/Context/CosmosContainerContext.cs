//using System.Net;
//using Emporium.Domain.SeedWork;
//using Mediator;
//using Microsoft.Azure.Cosmos;
//using Emporium.Infrastructure.Exceptions;

//namespace Emporium.Infrastructure.Context;

//public class CosmosContainerContext : IContainerContext
//{
//    private readonly IMediator mediator;

//    public CosmosContainerContext(Container container, IMediator mediator)
//    {
//        Container = container;
//        this.mediator = mediator;
//    }

//    // for multi-threading, this should be migrated to one of the 
//    // concurrent collections of C# 
//    public List<IDataObject<IEntity>> DataObjects { get; } = new();

//    public void Reset()
//    {
//        DataObjects.Clear();
//    }

//    public Container Container { get; }

//    public void Add(IDataObject<IEntity> entity)
//    {
//        if (DataObjects.FindIndex(0,
//                o => o.Id == entity.Id && o.PartitionKey == entity.PartitionKey) == -1)
//            DataObjects.Add(entity);
//    }

//    public async Task<List<IDataObject<IEntity>>> SaveChangesAsync(CancellationToken cancellationToken = default)
//    {
//        RaiseDomainEvents(DataObjects);

//        switch (DataObjects.Count)
//        {
//            case 1:
//                {
//                    var result = await SaveSingleAsync(DataObjects[0], cancellationToken);
//                    return result;
//                }
//            case > 1:
//                {
//                    var result = await SaveInTransactionalBatchAsync(cancellationToken);
//                    return result;
//                }
//            default:
//                return new List<IDataObject<IEntity>>();
//        }
//    }

//    private async Task<List<IDataObject<IEntity>>> SaveInTransactionalBatchAsync(
//        CancellationToken cancellationToken)
//    {
//        if (DataObjects.Count > 0)
//        {
//            var pk = new PartitionKey(DataObjects[0].PartitionKey);
//            var tb = Container.CreateTransactionalBatch(pk);
//            DataObjects.ForEach(o =>
//            {
//                TransactionalBatchItemRequestOptions tro = null;

//                if (!string.IsNullOrWhiteSpace(o.Etag))
//                    tro = new TransactionalBatchItemRequestOptions { IfMatchEtag = o.Etag };

//                switch (o.State)
//                {
//                    case EntityState.Created:
//                        tb.CreateItem(o);
//                        break;
//                    case EntityState.Updated or EntityState.Deleted:
//                        tb.ReplaceItem(o.Id, o, tro);
//                        break;
//                }
//            });

//            var tbResult = await tb.ExecuteAsync(cancellationToken);

//            if (!tbResult.IsSuccessStatusCode)
//                for (var i = 0; i < DataObjects.Count; i++)
//                    if (tbResult[i].StatusCode != HttpStatusCode.FailedDependency)
//                    {
//                        // Not recoverable - clear context
//                        DataObjects.Clear();
//                        throw EvaluateCosmosError(tbResult[i].StatusCode);
//                    }

//            for (var i = 0; i < DataObjects.Count; i++)
//                DataObjects[i].Etag = tbResult[i].ETag;
//        }

//        var result = new List<IDataObject<IEntity>>(DataObjects); // return copy of list as result

//        // work has been successfully done - reset DataObjects list
//        DataObjects.Clear();
//        return result;
//    }

//    private async Task<List<IDataObject<IEntity>>> SaveSingleAsync(IDataObject<IEntity> dObj,
//        CancellationToken cancellationToken = default)
//    {
//        var reqOptions = new ItemRequestOptions
//        {
//            EnableContentResponseOnWrite = false
//        };

//        if (!string.IsNullOrWhiteSpace(dObj.Etag)) reqOptions.IfMatchEtag = dObj.Etag;

//        var pk = new PartitionKey(dObj.PartitionKey);

//        try
//        {
//            ItemResponse<IDataObject<IEntity>> response;

//            switch (dObj.State)
//            {
//                case EntityState.Created:
//                    response = await Container.CreateItemAsync(dObj, pk, reqOptions, cancellationToken);
//                    break;
//                case EntityState.Updated:
//                case EntityState.Deleted:
//                    response = await Container.ReplaceItemAsync(dObj, dObj.Id, pk, reqOptions, cancellationToken);
//                    break;
//                default:
//                    DataObjects.Clear();
//                    return new List<IDataObject<IEntity>>();
//            }

//            dObj.Etag = response.ETag;
//            var result = new List<IDataObject<IEntity>>(1) { dObj };

//            // work has been successfully done - reset DataObjects list
//            DataObjects.Clear();
//            return result;
//        }
//        catch (CosmosException e)
//        {
//            // Not recoverable - clear context
//            DataObjects.Clear();
//            throw EvaluateCosmosError(e, Guid.Parse(dObj.Id), dObj.Etag);
//        }
//    }

//    private void RaiseDomainEvents(List<IDataObject<IEntity>> dObjs)
//    {
//        //var eventEmitters = new List<IEventEmitter<IEvent>>();
//        var eventEmitters = new List<IEntity>();

//        // Get all EventEmitters
//        foreach (var o in dObjs)
//            //if (o.Data is IEventEmitter<IEvent> ee)
//                //eventEmitters.Add(ee);
//                 eventEmitters.Add(o.Data);

//        // Raise Events
//        if (eventEmitters.Count <= 0) return;
//        foreach (var evt in eventEmitters.SelectMany(eventEmitter => eventEmitter.DomainEvents))
//        {
//            //mediator.Publish(evt);
//            var e = new DataObject<DomainEventBase>(evt.Id.ToString(), evt.RelatedId.ToString(), "domainEvent", evt as DomainEventBase, null,
//            120, EntityState.Created);
//            Add(e);
//        }

//        foreach (var entity in eventEmitters)
//        {
//            entity.ClearDomainEvents();
//        }
//    }

//    private Exception EvaluateCosmosError(CosmosException error, Guid? id = null, string etag = null)
//    {
//        return EvaluateCosmosError(error.StatusCode, id, etag);
//    }

//    private Exception EvaluateCosmosError(HttpStatusCode statusCode, Guid? id = null, string etag = null)
//    {
//        return statusCode switch
//        {
//            HttpStatusCode.NotFound => new DomainObjectNotFoundException(
//                $"Domain object not found for Id: {(id != null ? id.Value : string.Empty)} / ETag: {etag}"),
//            HttpStatusCode.NotModified => new DomainObjectNotModifiedException(
//                $"Domain object not modified. Id: {(id != null ? id.Value : string.Empty)} / ETag: {etag}"),
//            HttpStatusCode.Conflict => new DomainObjectConflictException(
//                $"Domain object conflict detected. Id: {(id != null ? id.Value : string.Empty)} / ETag: {etag}"),
//            HttpStatusCode.PreconditionFailed => new DomainObjectPreconditionFailedException(
//                $"Domain object mid-air collision detected. Id: {(id != null ? id.Value : string.Empty)} / ETag: {etag}"),
//            HttpStatusCode.TooManyRequests => new DomainObjectTooManyRequestsException(
//                "Too many requests occurred. Try again later)"),
//            _ => new Exception("Cosmos Exception")
//        };
//    }
//}