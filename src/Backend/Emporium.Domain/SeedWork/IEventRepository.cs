namespace Emporium.Domain.SeedWork;

public interface IEventRepository
{
    public void Create(IDomainEvent e);
}
