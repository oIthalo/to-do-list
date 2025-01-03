using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.TodoTask;

public interface ICreateTodoTaskUseCase
{
    Task<TaskResponse> Execute(CreateTaskRequest request);
}