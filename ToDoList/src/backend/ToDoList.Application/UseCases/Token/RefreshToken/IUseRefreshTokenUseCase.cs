using ToDoList.Communication.Requests;

namespace ToDoList.Application.UseCases.Token.RefreshToken;

public interface IUseRefreshTokenUseCase
{
    Task Execute(NewTokenRequest request);
}