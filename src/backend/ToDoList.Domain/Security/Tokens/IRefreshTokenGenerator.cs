namespace ToDoList.Domain.Security.Tokens;

public interface IRefreshTokenGenerator
{
    string Generate();
}