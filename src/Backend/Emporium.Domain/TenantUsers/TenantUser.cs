using Emporium.Domain.SeedWork;

namespace Emporium.Domain.TenantUsers;
public class TenantUser : GlobalEntity<TenantUserId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Login { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    private readonly List<TenantId> tenantRegistrations = new();
    public IReadOnlyCollection<TenantId> TenantRegistrations => tenantRegistrations.AsReadOnly();

    private TenantUser() : base() { }

    private TenantUser(TenantUserId id, string name, string lastName, string email, string login, string password) : base(id)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Login = login;
        Password = password;
    }
    
    public static TenantUser CreateUser(
    string name
    , string lastName
    , string email
    , string login
    , string password)
    {
        var user = new TenantUser(new TenantUserId(Guid.NewGuid()), name, lastName, email, login, password);
        //UserValidator.ValidateUser(user);

        return user;
    }

    public void AddTenantRegistration(TenantId tenantId)
    {
        if (!tenantRegistrations.Contains(tenantId))
        {
            tenantRegistrations.Add(tenantId);
        }
    }

    public void RemoveTenantRegistration(TenantId tenantId)
    {
        tenantRegistrations.Remove(tenantId);
    }
}
