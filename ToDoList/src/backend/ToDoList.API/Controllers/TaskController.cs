using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Attributes;
using ToDoList.Application.UseCases.TodoTask.Create;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.API.Controllers;

public class TaskController : ToDoListControllerBase
{
    [HttpPost]
    [Route("create")]
    [IsAuth]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(
        [FromServices] ICreateTodoTaskUseCase useCase,
        [FromBody] CreateTaskRequest request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }
}