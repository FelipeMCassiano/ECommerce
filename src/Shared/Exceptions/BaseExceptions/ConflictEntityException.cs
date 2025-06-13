using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Exceptions.BaseExceptions;

public class ConflictEntityException: ECommerceException
{
    public ConflictEntityException(string message) : base(message)
    {
    }

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetHttpStatus()
    {
        return HttpStatusCode.Conflict;
    }
}