using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.TodoTask.Update;
using ToDoList.Exception;

namespace Validators.Test.TodoTasks.Update;

public class UpdateTodoTaskValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = UpdateTaskRequestBuilder.Build();
        var result = new UpdateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Error_Title_Empty()
    {
        var request = UpdateTaskRequestBuilder.Build();
        request.Title = string.Empty;

        var result = new UpdateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.TITLE_EMPTY));
    }

    [Fact]
    public void Error_Description_Empty()
    {
        var request = UpdateTaskRequestBuilder.Build();
        request.Description = string.Empty;

        var result = new UpdateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.DESCRIPTION_EMPTY));
    }

    [Fact]
    public void Error_Description_TooLong()
    {
        var request = UpdateTaskRequestBuilder.Build();
        request.Description = new string('A', 501);

        var result = new UpdateTodoTaskValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.DESCRIPTION_INVALID));
    }
}
