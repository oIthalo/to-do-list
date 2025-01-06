using ToDoList.Domain.Security.Tokens;

namespace ToDoList.Infrastructure.Security.Tokens.Refresh.Generator;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
}