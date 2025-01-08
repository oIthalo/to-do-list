using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ToDoList.Application.UseCases.TodoTask.Delete;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.TodoTask.Delete;

public class DeleteTaskUseCaseTest
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

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(999);

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(x => x.Message.Equals(MessagesException.TASK_NOT_FOUND));
    }

    private static DeleteTaskUseCase CreateUseCase(
        ToDoList.Domain.Entities.User user,
        ToDoList.Domain.Entities.TodoTask? todoTask = default)
    {
        var todoTaskRepository = new TodoTaskRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        if (todoTask is not null)
            todoTaskRepository.GetById(user, todoTask);

        return new DeleteTaskUseCase(todoTaskRepository.Build(), unitOfWork, loggedUser);
    }
}
