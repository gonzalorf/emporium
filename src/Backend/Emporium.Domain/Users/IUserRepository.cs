namespace Emporium.Domain.Users;

public interface IUserRepository
{
    Task Add(User user);
    void Remove(User user);
    void Update(User user);
    Task<User?> GetById(UserId id);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByLogin(string login);
}
