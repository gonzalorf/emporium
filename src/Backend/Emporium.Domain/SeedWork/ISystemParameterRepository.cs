namespace Emporium.Domain.SeedWork;
public interface ISystemParameterRepository
{
    Task<T> Get<T>(string name);
}
