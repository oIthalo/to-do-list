using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.UseCases.Token.RefreshToken;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.API.Controllers;

public class TokenController : ToDoListControllerBase
{
    [HttpPost]
    [Route("refresh-token")]
    [ProducesResponseType(typeof(TokensResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(
        [FromServices] IUseRefreshTokenUseCase useCase,
        [FromBody] NewTokenRequest request)
    {
        var result = await useCase.Execute(request);
        return Ok(result);
    }
}