using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Delete;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.User.Delete;

public class DeleteUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = ChangeTaskStatusRequestBuilder.Build();
        (var user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute();

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        var request = ChangeTaskStatusRequestBuilder.Build();
        (var user, _) = UserBuilder.Build();

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute();

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(x => x.Message.Equals(MessagesException.USER_NOT_FOUND));
    }

    private static DeleteUserUseCase CreateUseCase(ToDoList.Domain.Entities.User? user = default!)
    {
        (var userBuilded, _) = UserBuilder.Build();
        var userRepository = new UserRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(userBuilded);
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (user is not null)
            userRepository.GetByIdentifier(user);

        return new DeleteUserUseCase(userRepository.Build(), loggedUser, unitOfWork);
    }
}