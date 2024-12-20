namespace ToDoList.Communication.Responses;

public class ErrorsResponse
{
    public ErrorsResponse(IList<string> errorMessages) =>
        ErrorMessages = errorMessages;

    public ErrorsResponse(string errorMessage) =>
        ErrorMessages = new List<string> { errorMessage };

    public IList<string> ErrorMessages { get; set; } = [];
}