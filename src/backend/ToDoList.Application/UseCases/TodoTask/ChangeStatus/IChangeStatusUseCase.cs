using ToDoList.Communication.Requests;

namespace ToDoList.Application.UseCases.TodoTask.ChangeStatus;

public interface IChangeStatusUseCase
{
    Task Execute(long id, ChangeStatusRequest request);
}