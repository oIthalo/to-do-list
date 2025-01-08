using CommonTestUtilities.Criptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Password.Change;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.User.ChangePassword;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var request = PasswordChangeRequestBuilder.Build();
        request.Password = password;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        (var user, var password) = UserBuilder.Build();

        var request = PasswordChangeRequestBuilder.Build();
        request.NewPassword = string.Empty;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(x => x.ErrorMessages
            .Contains(MessagesException.PASSWORD_EMPTY));
    }

    private static PasswordChangeUseCase CreateUseCase(ToDoList.Domain.Entities.User user)
    {
        var userRepository = new UserRepositoryBuilder().Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new PasswordChangeUseCase(userRepository, loggedUser, passwordEncripter, unitOfWork);
    }
}