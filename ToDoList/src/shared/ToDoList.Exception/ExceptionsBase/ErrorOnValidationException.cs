namespace ToDoList.Exception.ExceptionsBase;

public class ErrorOnValidationException : ToDoListException
{
    public ErrorOnValidationException(IList<string> errorMessages) =>
        ErrorMessages = errorMessages;

    public IList<string> ErrorMessages { get; set; }
}