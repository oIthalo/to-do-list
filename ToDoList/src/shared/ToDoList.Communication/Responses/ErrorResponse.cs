namespace ToDoList.Communication.Responses;

public class ErrorResponse
{
    public ErrorResponse(IList<string> errorMessages) =>
        ErrorMessages = errorMessages;

    public ErrorResponse(string errorMessage) =>
        ErrorMessages = new List<string> { errorMessage };

    public IList<string> ErrorMessages { get; set; } = [];
    public bool TokenIsExpired { get; set; } = false;
}