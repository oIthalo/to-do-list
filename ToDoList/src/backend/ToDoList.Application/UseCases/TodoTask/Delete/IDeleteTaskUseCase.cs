namespace ToDoList.Application.UseCases.TodoTask.Delete;

public interface IDeleteTaskUseCase
{
    Task Execute(long id);
}