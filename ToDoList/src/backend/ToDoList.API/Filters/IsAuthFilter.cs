using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.API.Filters;

public class IsAuthFilter : IAsyncAuthorizationFilter
{
    private readonly ITokenValidator _tokenValidator;
    private readonly IUserRepository _userRepository;

    public IsAuthFilter(
        ITokenValidator tokenValidator, 
        IUserRepository userRepository)
    {
        _tokenValidator = tokenValidator;
        _userRepository = userRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _tokenValidator.ValidateAndGetUserIdentifier(token);

            var user = await _userRepository.GetByIdentifer(userIdentifier) ?? throw new UnauthorizedException(MessagesException.USER_WITHOUT_PERMISSION);
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse("Token is expired.")
            {
                TokenIsExpired = true,
            });
        }
        catch (ToDoListException toDoListException)
        {
            context.HttpContext.Response.StatusCode = (int)toDoListException.GetStatusCode();
            context.Result = new ObjectResult(new ErrorResponse(toDoListException.GetErrorMessages()));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(MessagesException.USER_WITHOUT_PERMISSION));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authentication))  
            throw new UnauthorizedException(MessagesException.NO_TOKEN);

        return authentication["Bearer ".Length..].Trim();
    }
}