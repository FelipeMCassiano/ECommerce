using System.Net;
using Exceptions.MessageExceptions;

namespace Exceptions.BaseExceptions;

public class InvalidUserCredentialsException : ECommerceException
{
    public InvalidUserCredentialsException() : base(UserExceptionMessages.InvalidUserCredentials)
    {
    }

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetHttpStatus()
    {
        return HttpStatusCode.Unauthorized;
    }
}