﻿using CommonTestUtilities.Requests;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Register;
using ToDoList.Exception;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        var result = new RegisterUserValidator().Validate(request);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Name = string.Empty;

        var result = new RegisterUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.USER_NAME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Email = string.Empty;

        var result = new RegisterUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Email = "invalidemail.com";

        var result = new RegisterUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.EMAIL_INVALID));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Password = string.Empty;

        var result = new RegisterUserValidator().Validate(request);

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
        var request = RegisterUserRequestBuilder.Build(passwordLength);
        var result = new RegisterUserValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(MessagesException.SHORT_PASSWORD));
    }
}