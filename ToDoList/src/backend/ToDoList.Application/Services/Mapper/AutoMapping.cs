using AutoMapper;
using Sqids;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Mapper;

public class AutoMapping : Profile
{
    private readonly SqidsEncoder<long> _idEncoder;

    public AutoMapping(SqidsEncoder<long> idEncoder)
    {
        _idEncoder = idEncoder;

        RequestToDomain();
        DomainToRespone();
    }

    private void RequestToDomain()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<DoLoginRequest, User>();
        CreateMap<CreateTaskRequest, TodoTask>();
        CreateMap<UpdateTaskRequest, TodoTask>();
    }

    private void DomainToRespone()
    {
        CreateMap<User, UserProfileResponse>();

        CreateMap<TodoTask, TaskResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => _idEncoder.Encode(src.Id)));
    }
}