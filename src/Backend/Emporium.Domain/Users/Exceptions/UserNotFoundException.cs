namespace Emporium.Domain.Users.Exceptions;

public sealed class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string login) : base($"User with login {login} not found.")
    {
    }
}
