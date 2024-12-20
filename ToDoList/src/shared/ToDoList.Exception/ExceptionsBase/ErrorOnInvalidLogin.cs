namespace ToDoList.Exception.ExceptionsBase;

public class ErrorOnInvalidLogin : ToDoListException
{
    public ErrorOnInvalidLogin() : base(MessagesException.LOGIN_INVALID)
    {
    }
}