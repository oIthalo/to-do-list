using CommonTestUtilities.AutoMapper;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.TodoTask.Create;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace UseCases.Test.TodoTask.Create;

public class CreateTodoTaskUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();

        var request = CreateTaskRequestBuilder.Build();
        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        (var user, _) = UserBuilder.Build();

        var request = CreateTaskRequestBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<ErrorOnValidationException>()
            .Where(x => x.ErrorMessages.Count == 1 && x.ErrorMessages
            .Contains(MessagesException.TITLE_EMPTY));
    }

    private static CreateTodoTaskUseCase CreateUseCase(ToDoList.Domain.Entities.User user)
    {
        var todoTaskRepository = new TodoTaskRepositoryBuilder().Build();
        var autoMapper = AutoMapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new CreateTodoTaskUseCase(todoTaskRepository, autoMapper, unitOfWork, loggedUser);
    }
}