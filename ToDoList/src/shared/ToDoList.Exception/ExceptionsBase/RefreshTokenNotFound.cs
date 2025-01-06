using System.Net;

namespace ToDoList.Exception.ExceptionsBase;

public class RefreshTokenNotFound : ToDoListException
{
    public RefreshTokenNotFound() : base(MessagesException.REFRESH_TOKEN_NOT_FOUND)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}