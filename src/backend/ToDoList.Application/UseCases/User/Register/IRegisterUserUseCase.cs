using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<RegisterUserResponse> Execute(RegisterUserRequest request);
}