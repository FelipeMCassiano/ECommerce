using System.Net;
using System.Runtime.InteropServices.JavaScript;

namespace Exceptions.BaseExceptions;

public abstract class ECommerceException : Exception
{
    public ECommerceException(string message) : base(message)
    {
        
    }

    public abstract List<string> GetErrorMessages();

    public abstract HttpStatusCode GetHttpStatus();
}