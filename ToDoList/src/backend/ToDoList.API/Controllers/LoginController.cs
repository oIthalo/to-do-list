using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.UseCases.Login.DoLogin;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.API.Controllers;

public class LoginController : ToDoListControllerBase
{
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] DoLoginRequest request)
    {
        var result = await useCase.Execute(request);
        return Ok(result);
    }
}