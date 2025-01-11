using System.Net;

namespace ToDoList.Exception.ExceptionsBase;

public class ErrorOnInvalidLogin : ToDoListException
{
    public ErrorOnInvalidLogin() : base(MessagesException.LOGIN_INVALID)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}