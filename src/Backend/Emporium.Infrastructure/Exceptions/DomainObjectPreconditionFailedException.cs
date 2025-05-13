using System;

namespace Emporium.Infrastructure.Exceptions;

public class DomainObjectPreconditionFailedException : Exception
{
    public DomainObjectPreconditionFailedException()
    {
    }

    public DomainObjectPreconditionFailedException(string message) : base(message)
    {
    }

    public DomainObjectPreconditionFailedException(string message, Exception inner) : base(message, inner)
    {
    }
}