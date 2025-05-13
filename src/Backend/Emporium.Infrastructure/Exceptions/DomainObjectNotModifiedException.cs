using System;

namespace Emporium.Infrastructure.Exceptions;

public class DomainObjectNotModifiedException : Exception
{
    public DomainObjectNotModifiedException()
    {
    }

    public DomainObjectNotModifiedException(string message) : base(message)
    {
    }

    public DomainObjectNotModifiedException(string message, Exception inner) : base(message, inner)
    {
    }
}