namespace Emporium.Application.Services;
public interface IPasswordHashService
{
    string Hash(string password);
}
