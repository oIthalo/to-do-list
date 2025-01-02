using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    Task<UserProfileResponse> Execute();
}