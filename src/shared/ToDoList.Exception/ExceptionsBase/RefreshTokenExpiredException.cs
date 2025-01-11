using System.Net;

namespace ToDoList.Exception.ExceptionsBase;

public class RefreshTokenExpiredException : ToDoListException
{
    public RefreshTokenExpiredException() : base(MessagesException.REFRESH_TOKEN_EXPIRED)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}