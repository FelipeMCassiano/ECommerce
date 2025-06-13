using System.Net;
using Exceptions.MessageExceptions;

namespace Exceptions.BaseExceptions;

public class NotFoundUserException : ECommerceException

{
    public NotFoundUserException() : base(UserExceptionMessages.NotFoundUser
    )
    {
    }

    public override List<string> GetErrorMessages(){
       return [Message];
    }


    public override HttpStatusCode GetHttpStatus()
    {
        return HttpStatusCode.NotFound;
    }
}