using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Update;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.User.Update;

public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();

        var request = UpdateUserRequestBuilder.Build();
        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = UpdateUserRequestBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(x => x.ErrorMessages.Count == 1 && x.ErrorMessages
            .Contains(MessagesException.USER_NAME_EMPTY));
    }

    private static UpdateUserUseCase CreateUseCase(ToDoList.Domain.Entities.User? user = default)
    {
        var userRepository = new UserRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(user!);
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (user is not null)
            userRepository.GetByIdentifier(user);

        return new UpdateUserUseCase(userRepository.Build(), loggedUser, unitOfWork);
    }
}