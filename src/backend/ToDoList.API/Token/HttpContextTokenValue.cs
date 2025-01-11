using ToDoList.Domain.Security.Tokens;

namespace ToDoList.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

    public string Value()
    {
        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization["Bearer ".Length..].Trim();
    }
}