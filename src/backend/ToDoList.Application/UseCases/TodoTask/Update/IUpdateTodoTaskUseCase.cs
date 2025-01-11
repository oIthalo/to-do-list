using ToDoList.Communication.Requests;

namespace ToDoList.Application.UseCases.TodoTask.Update;

public interface IUpdateTodoTaskUseCase
{
    Task Execute(long id, UpdateTaskRequest request);
}