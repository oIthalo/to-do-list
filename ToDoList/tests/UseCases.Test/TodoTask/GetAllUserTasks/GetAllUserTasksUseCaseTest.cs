using CommonTestUtilities.AutoMapper;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using ToDoList.Application.UseCases.TodoTask.GetAllUserTasks;
using ToDoList.Communication.Responses;

namespace UseCases.Test.TodoTask.GetAllUserTasks;

public class GetAllUserTasksUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Should().BeOfType<TasksResponse>();
    }

    private static GetAllUserTasksUseCase CreateUseCase(ToDoList.Domain.Entities.User user)
    {
        var todoTaskRepository = new TodoTaskRepositoryBuilder();
        var autoMapper = AutoMapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        todoTaskRepository.GetAllTaskUsers(user);

        return new GetAllUserTasksUseCase(todoTaskRepository.Build(), autoMapper, loggedUser);
    }
}
