using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.TodoTask.Create;
using ToDoList.Exception;

namespace Validators.Test.TodoTasks.Create;

public class CreateTodoTaskValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = CreateTaskRequestBuilder.Build();
        var result = new CreateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Error_Title_Empty()
    {
        var request = CreateTaskRequestBuilder.Build();
        request.Title = string.Empty;

        var result = new CreateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.TITLE_EMPTY));
    }

    [Fact]
    public void Error_Description_Empty()
    {
        var request = CreateTaskRequestBuilder.Build();
        request.Description = string.Empty;

        var result = new CreateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.DESCRIPTION_EMPTY));
    }

    [Fact]
    public void Error_Description_Too_Long()
    {
        var request = CreateTaskRequestBuilder.Build();
        request.Description = new string('A', 501);

        var result = new CreateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.DESCRIPTION_INVALID));
    }
}
