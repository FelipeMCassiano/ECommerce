using System.Net;

namespace Exceptions.BaseExceptions;

public class ErrorOnValidationException : ECommerceException
{
    private readonly List<string> _errors;
    public ErrorOnValidationException(List<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }

    public override List<string> GetErrorMessages()
    {
        return _errors;
    }

    public override HttpStatusCode GetHttpStatus()
    {
        return HttpStatusCode.BadRequest;
    }
}