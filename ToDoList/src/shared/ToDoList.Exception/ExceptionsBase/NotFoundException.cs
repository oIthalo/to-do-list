using System.Net;

namespace ToDoList.Exception.ExceptionsBase;

public class NotFoundException : ToDoListException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}