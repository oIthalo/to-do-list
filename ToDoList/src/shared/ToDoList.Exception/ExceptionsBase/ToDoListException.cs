using System.Net;

namespace ToDoList.Exception.ExceptionsBase;

public abstract class ToDoListException : SystemException
{
    public ToDoListException(string message) 
        : base (message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}