using AutoMapper;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;

namespace ToDoList.Application.UseCases.TodoTask.GetAllUserTasks;

public class GetAllUserTasksUseCase : IGetAllUserTasksUseCase
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllUserTasksUseCase(
        ITodoTaskRepository todoTaskRepository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _todoTaskRepository = todoTaskRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<IList<TaskResponse>> Execute()
    {
        var user = await _loggedUser.User();

        var tasks = await _todoTaskRepository.GetAllUserTasks(user);

        return _mapper.Map<IList<TaskResponse>>(tasks);
    }
}