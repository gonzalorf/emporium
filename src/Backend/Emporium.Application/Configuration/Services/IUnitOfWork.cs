namespace Emporium.Application.Configuration.Services;

public interface IUnitOfWork : IAsyncDisposable
{
    // Guarda todos los cambios registrados atómicamente (si pertenecen al mismo
    // contenedor y partition key) y publica eventos de dominio a través del Outbox.
    // Lanza una excepción si se intentan guardar cambios de múltiples
    // contenedores o partition keys en la misma llamada.
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

    // Nota: Los métodos para registrar cambios (Add, Update, Delete)
    // ahora son internos a la implementación del UoW y son llamados
    // por las implementaciones concretas de los repositorios.
}