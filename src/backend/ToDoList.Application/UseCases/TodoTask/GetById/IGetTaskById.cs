using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.TodoTask.GetById;

public interface IGetTaskById
{
    Task<TaskResponse> Execute(long id);
}