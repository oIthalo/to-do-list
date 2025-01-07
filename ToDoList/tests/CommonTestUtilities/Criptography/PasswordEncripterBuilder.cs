using Moq;
using ToDoList.Domain.Security.Criptography;

namespace CommonTestUtilities.Criptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new Mock<IPasswordEncripter>().Object;
}