using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.Login.DoLogin;

public interface ILoginUseCase
{
    Task<RegisterUserResponse> Execute(DoLoginRequest request);
}