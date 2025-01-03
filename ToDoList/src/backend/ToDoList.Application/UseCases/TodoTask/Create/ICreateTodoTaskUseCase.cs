using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.TodoTask.Create;

public interface ICreateTodoTaskUseCase
{
    Task<TaskResponse> Execute(CreateTaskRequest request);
}