using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.TodoTask.Delete;

public class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteTaskUseCase(
        ITodoTaskRepository todoTaskRepository, 
        IUnitOfWork unitOfWork, 
        ILoggedUser loggedUser)
    {
        _todoTaskRepository = todoTaskRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var user = await _loggedUser.User();

        var task = await _todoTaskRepository.GetById(user, id) ?? throw new NotFoundException();

        _todoTaskRepository.Delete(task);
        await _unitOfWork.Commit();
    }
}