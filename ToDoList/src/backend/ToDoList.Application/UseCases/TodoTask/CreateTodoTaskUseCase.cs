using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.TodoTask;

public class CreateTodoTaskUseCase : ICreateTodoTaskUseCase
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public CreateTodoTaskUseCase(
        ITodoTaskRepository todoTaskRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _todoTaskRepository = todoTaskRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task<TaskResponse> Execute(CreateTaskRequest request)
    {
        Validate(request);

        var user = await _loggedUser.User();
        var task = _mapper.Map<Domain.Entities.TodoTask>(request);

        task.UserIdentifier = user.Identifier;

        await _todoTaskRepository.Add(task);
        await _unitOfWork.Commit();

        return _mapper.Map<TaskResponse>(task);
    }

    private static void Validate(CreateTaskRequest request)
    {
        var result = new CreateTodoTaskValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}