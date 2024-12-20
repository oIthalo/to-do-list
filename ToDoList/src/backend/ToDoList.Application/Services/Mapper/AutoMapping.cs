using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Mapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
    }

    private void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<DoLoginRequest, User>();
    }
}