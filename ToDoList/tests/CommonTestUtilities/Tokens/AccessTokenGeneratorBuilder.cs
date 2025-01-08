using ToDoList.Domain.Security.Tokens;
using ToDoList.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens;

public class AccessTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => new GenerateAccessToken(120, "e5d220729f9d446f999e03fd494da507");
}