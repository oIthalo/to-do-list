using Moq;
using ToDoList.Domain.Security.Criptography;
using ToDoList.Infrastructure.Security.Criptography;

namespace CommonTestUtilities.Criptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new BCryptNet();
}