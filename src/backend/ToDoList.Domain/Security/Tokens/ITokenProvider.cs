namespace ToDoList.Domain.Security.Tokens;

public interface ITokenProvider
{
    string Value();
}