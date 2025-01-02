using AutoMapper;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Services.LoggedUser;

namespace ToDoList.Application.UseCases.User.Profile;

public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetUserProfileUseCase(
        ILoggedUser loggedUser, 
        IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<UserProfileResponse> Execute()
    {
        var user = await _loggedUser.User();
        return _mapper.Map<UserProfileResponse>(user);
    }
}