using CommonTestUtilities.AutoMapper;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.TodoTask.Update;
using ToDoList.Exception.ExceptionsBase;
using ToDoList.Exception;
using ToDoList.Application.UseCases.TodoTask.GetById;

namespace UseCases.Test.TodoTask.GetById;

public class GetTaskByIdTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var todoTask = TodoTaskBuilder.Build(user);

        var useCase = CreateUseCase(user, todoTask);

        Func<Task> act = async () => await useCase.Execute(todoTask.Id);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Task_Not_Found()
    {
        (var user, _) = UserBuilder.Build();
        var todoTask = TodoTaskBuilder.Build(user);

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(todoTask.Id);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(x => x.Message.Equals(MessagesException.TASK_NOT_FOUND));
    }

    private static GetTaskById CreateUseCase(
        ToDoList.Domain.Entities.User user,
        ToDoList.Domain.Entities.TodoTask? todoTask = default)
    {
        var todoTaskRepository = new TodoTaskRepositoryBuilder();
        var autoMapper = AutoMapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        if (todoTask is not null)
            todoTaskRepository.GetById(user, todoTask);

        return new GetTaskById(todoTaskRepository.Build(), autoMapper, loggedUser);
    }
}