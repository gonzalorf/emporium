using Emporium.Domain.Providers.Validators;

namespace Emporium.Domain.Providers;

public class Provider : AuditableEntity<ProviderId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string BankAccountNumber { get; private set; } = string.Empty;
    public string BankAccountAlias { get; private set; } = string.Empty;

    private Provider(){}

    private Provider(ProviderId id, string name, string bankAccountNumber, string bankAccountAlias) : base(id)
    {
        Name = name;
        BankAccountNumber = bankAccountNumber;
        BankAccountAlias = bankAccountAlias;
    }

    public static Provider CreateProvider(string name, string bankAccountNumber, string bankAccountAlias)
    {
        var provider = new Provider(new ProviderId(Guid.NewGuid()), name, bankAccountNumber, bankAccountAlias);

        ProviderValidator.ValidateProvider(provider);

        return provider;
    }

    public void Update(string name, string bankAccountNumber, string bankAccountAlias)
    {
        Name = name;
        BankAccountNumber = bankAccountNumber;
        BankAccountAlias = bankAccountAlias;

        ProviderValidator.ValidateProvider(this);
    }
}
