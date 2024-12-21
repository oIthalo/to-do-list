namespace ToDoList.Domain.Security.Criptography;

public interface IPasswordEncripter
{
    string Encrypt(string password);
}