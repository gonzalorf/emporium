using Emporium.Application.Services;
using System.Security.Cryptography;
using System.Text;

namespace Emporium.Infrastructure.Services;
internal class PasswordHashService : IPasswordHashService
{
    public string Hash(string password)
    {
        using var sha384Hash = SHA384.Create();
        var sourceBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha384Hash.ComputeHash(sourceBytes);
        var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        return hash;
    }
}
