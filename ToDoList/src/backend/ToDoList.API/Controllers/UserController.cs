using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Attributes;
using ToDoList.Application.UseCases.User.Password.Change;
using ToDoList.Application.UseCases.User.Profile;
using ToDoList.Application.UseCases.User.Register;
using ToDoList.Application.UseCases.User.Update;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.API.Controllers;

public class UserController : ToDoListControllerBase
{
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RegisterUserRequest request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("update")]
    [IsAuth]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] UpdateUserRequest request)
    {
        await useCase.Execute(request);
        return NoContent();
    }

    [HttpGet]
    [Route("profile")]
    [IsAuth]
    [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Profile(
        [FromServices] IGetUserProfileUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }


    [HttpPost]
    [Route("change-password")]
    [IsAuth]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IPasswordChangeUseCase useCase,
        [FromBody] PasswordChangeRequest request)
    {
        await useCase.Execute(request);
        return NoContent();
    }
}