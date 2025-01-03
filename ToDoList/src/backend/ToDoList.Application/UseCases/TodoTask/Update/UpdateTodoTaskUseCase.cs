using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.TodoTask.Update;

public class UpdateTodoTaskUseCase : IUpdateTodoTaskUseCase
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public UpdateTodoTaskUseCase(
        ITodoTaskRepository todoTaskRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _todoTaskRepository = todoTaskRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id, UpdateTaskRequest request)
    {
        var user = await _loggedUser.User();

        Validate(request);

        var taskToUpdate = await _todoTaskRepository.GetById(user, id) ?? throw new NotFoundException(); ;
        var task = _mapper.Map(request, taskToUpdate);

        _todoTaskRepository.Update(task);
        await _unitOfWork.Commit();
    }

    private void Validate(UpdateTaskRequest request)
    {
        var result = new UpdateTodoTaskValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}