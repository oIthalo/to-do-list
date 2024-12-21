using ToDoList.Domain.Security.Criptography;

namespace ToDoList.Infrastructure.Security.Criptography;

public class BCryptNet : IPasswordEncripter
{
    public string Encrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}