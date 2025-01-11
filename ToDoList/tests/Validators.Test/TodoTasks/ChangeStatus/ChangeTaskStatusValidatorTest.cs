using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.TodoTask.ChangeStatus;
using ToDoList.Communication.Enums;
using ToDoList.Exception;

namespace Validators.Test.TodoTasks.ChangeStatus;

public class ChangeTaskStatusValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = ChangeTaskStatusRequestBuilder.Build();
        var result = new ChangeStatusValidator().Validate(request);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Error_Invalid_Status()
    {
        var request = ChangeTaskStatusRequestBuilder.Build();
        request.Status = (ETaskStatus)999;

        var result = new ChangeStatusValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.STATUS_INVALID));
    }
}