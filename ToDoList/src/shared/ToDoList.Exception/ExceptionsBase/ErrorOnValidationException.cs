namespace ToDoList.Exception.ExceptionsBase;

public class ErrorOnValidationException : ToDoListException
{
    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty) =>
        ErrorMessages = errorMessages;

    public IList<string> ErrorMessages { get; set; }
}