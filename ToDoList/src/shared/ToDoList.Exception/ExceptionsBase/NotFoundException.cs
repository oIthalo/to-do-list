namespace ToDoList.Exception.ExceptionsBase;

public class NotFoundException : ToDoListException
{
    public NotFoundException() : base(MessagesException.NOT_FOUND)
    {
    }
}