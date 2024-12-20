using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.UseCases.User.Register;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.API.Controllers;

public class UserController : ToDoListControllerBase
{
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RegisterUserRequest request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }
}