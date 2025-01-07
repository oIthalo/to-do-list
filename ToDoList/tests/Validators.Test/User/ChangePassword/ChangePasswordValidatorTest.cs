using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Password.Change;
using ToDoList.Exception;

namespace Validators.Test.User.ChangePassword;

public class PasswordChangeValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = PasswordChangeRequestBuilder.Build();
        var result = new PasswordChangeValidator().Validate(request);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var request = PasswordChangeRequestBuilder.Build();
        request.Password = string.Empty;

        var result = new PasswordChangeValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.INVALID_PASSWORD));
    }

    [Fact]
    public void Error_NewPassword_Empty()
    {
        var request = PasswordChangeRequestBuilder.Build();
        request.NewPassword = string.Empty;

        var result = new PasswordChangeValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.PASSWORD_EMPTY));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Error_Password_Invalid(int passwordLength)
    {
        var request = PasswordChangeRequestBuilder.Build(passwordLength);
        var result = new PasswordChangeValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.INVALID_PASSWORD));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Error_NewPassword_Invalid(int newPasswordLength)
    {
        var request = PasswordChangeRequestBuilder.Build(newPasswordLength: newPasswordLength);
        var result = new PasswordChangeValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.SHORT_PASSWORD));
    }
}
