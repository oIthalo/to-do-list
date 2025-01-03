using AutoMapper;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;

namespace ToDoList.Application.UseCases.TodoTask.GetById;

public class GetTaskById : IGetTaskById
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetTaskById(
        ITodoTaskRepository todoTaskRepository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _todoTaskRepository = todoTaskRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<TaskResponse> Execute(long id)
    {
        var user = await _loggedUser.User();
        var task = await _todoTaskRepository.GetById(user, id);

        return _mapper.Map<TaskResponse>(task);
    }
}