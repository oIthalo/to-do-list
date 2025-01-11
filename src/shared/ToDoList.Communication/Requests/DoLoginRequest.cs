namespace ToDoList.Communication.Requests;

public class DoLoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}