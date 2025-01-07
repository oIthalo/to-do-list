using ToDoList.Domain.Security.Tokens;
using ToDoList.Infrastructure.Security.Tokens.Refresh.Generator;

namespace CommonTestUtilities.Tokens;

public class RefreshTokenGeneratorBuilder
{
    public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
}