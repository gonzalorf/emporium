using Emporium.Domain.PlatformUsers;
using Emporium.Domain.TenantUsers;

namespace Emporium.Application.Configuration.Services;
public interface IJwtProvider
{
    string GetJwt(PlatformUser user);
    string GetJwt(TenantUser user);
}
