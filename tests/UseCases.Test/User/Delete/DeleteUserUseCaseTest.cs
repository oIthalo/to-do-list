﻿using CommonTestUtilities.Entities;
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

    private static DeleteUserUseCase CreateUseCase(ToDoList.Domain.Entities.User user = default!)
    {
        var userRepository = new UserRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (user is not null)
            userRepository.GetByIdentifier(user);

        return new DeleteUserUseCase(userRepository.Build(), loggedUser, unitOfWork);
    }
}