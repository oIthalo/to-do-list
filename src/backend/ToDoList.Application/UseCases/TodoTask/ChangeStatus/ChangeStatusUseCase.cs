using ToDoList.Communication.Requests;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.TodoTask.ChangeStatus;

public class ChangeStatusUseCase : IChangeStatusUseCase
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public ChangeStatusUseCase(
        ITodoTaskRepository todoTaskRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _todoTaskRepository = todoTaskRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id, ChangeTaskStatusRequest request)
    {
        Validate(request);

        var user = await _loggedUser.User();

        var task = await _todoTaskRepository.GetById(user, id) ?? throw new NotFoundException(MessagesException.TASK_NOT_FOUND);

        task.Status = (EStatusTask)request.Status;

        _todoTaskRepository.Update(task);
        await _unitOfWork.Commit();
    }

    private static void Validate(ChangeTaskStatusRequest request)
    {
        var result = new ChangeStatusValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}