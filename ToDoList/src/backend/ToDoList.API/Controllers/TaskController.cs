using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Attributes;
using ToDoList.API.Binders;
using ToDoList.Application.UseCases.TodoTask.Create;
using ToDoList.Application.UseCases.TodoTask.GetAllUserTasks;
using ToDoList.Application.UseCases.TodoTask.GetById;
using ToDoList.Application.UseCases.TodoTask.Update;
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

    [HttpGet]
    [Route("get/{id}")]
    [IsAuth]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetTaskById useCase,
        [FromRoute][ModelBinder(typeof(ToDoListIdBinder))] long id)
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-all")]
    [IsAuth]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllUserTasks(
        [FromServices] IGetAllUserTasksUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }

    [HttpPost]
    [Route("update/{id}")]
    [IsAuth]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateTodoTaskUseCase useCase,
        [FromRoute][ModelBinder(typeof(ToDoListIdBinder))] long id,
        [FromBody] UpdateTaskRequest request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
}