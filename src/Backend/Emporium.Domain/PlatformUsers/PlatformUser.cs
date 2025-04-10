namespace Emporium.Domain.PlatformUsers;
public class PlatformUser : Entity<PlatformUserId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Login { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public PlatformUserRole Role { get; private set; } = PlatformUserRole.Administrator;

    private PlatformUser() : base() { }

    private PlatformUser(PlatformUserId id, string name, string lastName, string email, string login, string password) : base(id)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Login = login;
        Password = password;
    }

    public static PlatformUser CreateUser(
    string name
    , string lastName
    , string email
    , string login
    , string password)
    {
        var user = new PlatformUser(new PlatformUserId(Guid.NewGuid()), name, lastName, email, login, password);
        //UserValidator.ValidateUser(user);

        return user;
    }

}
