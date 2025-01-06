using System.Net;

namespace ToDoList.Exception.ExceptionsBase;

public class NotFoundException : ToDoListException
{
    public NotFoundException() : base(MessagesException.NOT_FOUND)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}