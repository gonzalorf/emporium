using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Users;
public class User : Entity<UserId>, IAggregateRoot
{
    private User() : base() { }

    private User(UserId id, string name, string lastName, string email, string login, string password) : base(id)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Login = login;
        Password = password;
    }

    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Login { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public UserRole Role { get; private set; } = UserRole.Administrator;

    public static User CreateUser(
    string name
    , string lastName
    , string email
    , string login
    , string password)
    {
        var user = new User(new UserId(Guid.NewGuid()), name, lastName, email, login, password);
        //UserValidator.ValidateUser(user);

        return user;
    }

}
