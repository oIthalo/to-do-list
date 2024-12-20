namespace ToDoList.Communication.Responses;

public class RegisterUserResponse
{
    public string Name { get; set; } = string.Empty;
    public TokensResponse Tokens { get; set; } = default!;
}