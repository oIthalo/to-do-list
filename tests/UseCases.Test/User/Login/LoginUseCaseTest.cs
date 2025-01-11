using CommonTestUtilities.Criptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using ToDoList.Application.UseCases.Login.DoLogin;
using ToDoList.Communication.Requests;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.User.Login;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new DoLoginRequest()
        {
            Email = user.Email,
            Password = password
        });

        result.Should().NotBeNull();
        result.Tokens.Should().NotBeNull();
        result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
        result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var request = LoginRequestBuilder.Build();

        var useCase = CreateUseCase();

        Func<Task> act = async () => { await useCase.Execute(request); };

        await act.Should().ThrowAsync<ErrorOnInvalidLogin>()
            .Where(e => e.Message.Equals(MessagesException.LOGIN_INVALID));
    }

    private static LoginUseCase CreateUseCase(ToDoList.Domain.Entities.User? user = default)
    {
        var userRepository = new UserRepositoryBuilder();
        var accessTokenGenerator = AccessTokenGeneratorBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();
        var refreshTokenRepository = new RefreshTokenRepositoryBuilder().Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (user is not null)
            userRepository.GetByEmail(user);

        return new LoginUseCase(userRepository.Build(), accessTokenGenerator, passwordEncripter, refreshTokenGenerator, refreshTokenRepository, unitOfWork);
    }
}