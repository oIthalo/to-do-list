using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Mapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToRespone();
    }

    private void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<DoLoginRequest, User>();
        CreateMap<CreateTaskRequest, TodoTask>();
    }

    private void DomainToRespone()
    {
        CreateMap<User, UserProfileResponse>();
        CreateMap<TodoTask, TaskResponse>();
    }
}