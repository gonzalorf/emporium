using Emporium.Application.Common;
using Emporium.Application.Configuration.Commands;
using Emporium.Application.Configuration.Services;
using Emporium.Domain.Providers;

namespace Emporium.Application.Providers.Commands.CreateProvider;
internal class CreateProviderCommandHandler : ICommandHandler<CreateProviderCommand, Result<ProviderId>>
{
    private readonly IProviderRepository providerRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateProviderCommandHandler(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
    {
        this.providerRepository = providerRepository;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Result<ProviderId>> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = Provider.CreateProvider(
            request.Name,
            request.BankAccountNumber,
            request.BankAccountAlias
        );

        await providerRepository.Add(provider);

        var result = await unitOfWork.CommitAsync(cancellationToken);
        //var cResult = result.FirstOrDefault(r => r is DataObject<Contact>);
        //if (cResult != null)
        //{
        //    return new CreateContactCommandResponse(Guid.Parse(cResult.Id), cResult.Etag);
        //}

        return Result.Success(provider.Id);
    }
}