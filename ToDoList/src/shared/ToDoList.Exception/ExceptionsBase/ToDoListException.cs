namespace ToDoList.Exception.ExceptionsBase;

public class ToDoListException : SystemException
{
    public ToDoListException(string message) 
        : base (message) { }
}