namespace ToDoList.Domain.Security.Tokens;

public interface ITokenValidator
{
    Guid ValidateAndGetUserIdentifier(string token);
}