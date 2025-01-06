using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;

namespace ToDoList.Application.UseCases.Token.RefreshToken;

public interface IUseRefreshTokenUseCase
{
    Task<TokensResponse> Execute(NewTokenRequest request);
}