namespace ToDoList.Exception.ExceptionsBase;

public class ToDoListException : SystemException
{
    protected ToDoListException(string message) 
        : base (message) { }
}