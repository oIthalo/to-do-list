using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.TodoTask.GetAllUserTasks;

public interface IGetAllUserTasksUseCase
{
    Task<IList<TaskResponse>> Execute();
}