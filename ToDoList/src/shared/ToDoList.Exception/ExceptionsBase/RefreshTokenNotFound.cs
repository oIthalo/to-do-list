namespace ToDoList.Exception.ExceptionsBase;

public class RefreshTokenNotFound : ToDoListException
{
    public RefreshTokenNotFound() : base(MessagesException.REFRESH_TOKEN_NOT_FOUND)
    {
    }
}