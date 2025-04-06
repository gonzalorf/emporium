using Emporium.Domain.Users;

namespace Emporium.Application.Configuration.Services;
public interface IJwtProvider
{
    string GetJwt(User user);
}
