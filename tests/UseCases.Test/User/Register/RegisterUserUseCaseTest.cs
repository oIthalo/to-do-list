using CommonTestUtilities.AutoMapper;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Register;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Name.Should().NotBeNullOrWhiteSpace();
        result.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(x => x.ErrorMessages.Count == 1 && x.ErrorMessages.
            Contains(MessagesException.USER_NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Exist_User()
    {
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(x => x.ErrorMessages.Count == 1 && x.ErrorMessages.
            Contains(MessagesException.EXIST_USER));
    }

    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var userRepository = new UserRepositoryBuilder();
        var autoMapper = AutoMapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var accessTokenGenerator = AccessTokenGeneratorBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();
        var refreshTokenRepository = new RefreshTokenRepositoryBuilder().Build();

        if (email is not null)
            userRepository.ExistActiveUserWithEmail(email);

        return new RegisterUserUseCase(userRepository.Build(), autoMapper, unitOfWork, accessTokenGenerator, passwordEncripter, refreshTokenGenerator, refreshTokenRepository);
    }
}