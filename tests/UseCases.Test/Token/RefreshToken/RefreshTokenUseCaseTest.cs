using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using ToDoList.Application.UseCases.Token.RefreshToken;
using ToDoList.Communication.Requests;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.Token.RefreshToken;

public class RefreshTokenUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var refreshToken = RefreshTokenBuilder.Build(user);

        var useCase = CreateUseCase(refreshToken);
        var result = await useCase.Execute(new NewTokenRequest()
        {
            RefreshToken = refreshToken.Value,
        });

        result.RefreshToken.Should().NotBeNullOrWhiteSpace();
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Refresh_Token_Not_Found() 
    {

        var useCase = CreateUseCase();
        Func<Task> act = async () => await useCase.Execute(new NewTokenRequest()
        {
            RefreshToken = RefreshTokenGeneratorBuilder.Build().Generate()
        });

        await act.Should().ThrowAsync<RefreshTokenNotFound>()
            .Where(x => x.Message.Equals(MessagesException.REFRESH_TOKEN_NOT_FOUND));
    }

    private static UseRefreshTokenUseCase CreateUseCase(ToDoList.Domain.Entities.RefreshToken? refreshToken = default)
    {
        var tokenRepository = new RefreshTokenRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var accessTokenGenerator = AccessTokenGeneratorBuilder.Build();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();

        if (refreshToken is not null)
            tokenRepository.Get(refreshToken);

        return new UseRefreshTokenUseCase(tokenRepository.Build(), unitOfWork, accessTokenGenerator, refreshTokenGenerator);
    }
}