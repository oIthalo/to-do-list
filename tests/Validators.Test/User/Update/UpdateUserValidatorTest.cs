using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Update;
using ToDoList.Exception;

namespace Validators.Test.User.Update;

public class UpdateUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = UpdateUserRequestBuilder.Build();
        var result = new UpdateUserValidator().Validate(request);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var request = UpdateUserRequestBuilder.Build();
        request.Name = string.Empty;

        var result = new UpdateUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.USER_NAME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var request = UpdateUserRequestBuilder.Build();
        request.Email = string.Empty;

        var result = new UpdateUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var request = UpdateUserRequestBuilder.Build();
        request.Email = "invalidemail.com";

        var result = new UpdateUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.EMAIL_INVALID));
    }
}
