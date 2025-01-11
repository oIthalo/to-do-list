namespace ToDoList.Domain.Security.Criptography;

public interface IPasswordEncripter
{
    string Encrypt(string password);
    bool IsValid(string password, string passwordHash);
}