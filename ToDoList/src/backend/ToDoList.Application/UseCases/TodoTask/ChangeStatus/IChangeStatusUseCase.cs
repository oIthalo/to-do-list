using ToDoList.Domain.Enums;

namespace ToDoList.Application.UseCases.TodoTask.ChangeStatus;

public interface IChangeStatusUseCase
{
    Task Execute(long id, EStatusTask status);
}