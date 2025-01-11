using System.Net;

namespace ToDoList.Exception.ExceptionsBase;

public class UnauthorizedException : ToDoListException
{
    public UnauthorizedException(string message) : base(message) =>
        ErrorMessages= [message];

    public IList<string> ErrorMessages { get; set; }

    public override IList<string> GetErrorMessages() => ErrorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}