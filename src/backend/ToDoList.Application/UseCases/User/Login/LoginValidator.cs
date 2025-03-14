﻿using FluentValidation;
using ToDoList.Communication.Requests;
using ToDoList.Exception;

namespace ToDoList.Application.UseCases.Login.DoLogin;

public class LoginValidator : AbstractValidator<DoLoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(MessagesException.EMAIL_EMPTY);
        RuleFor(x => x.Password).NotEmpty().WithMessage(MessagesException.PASSWORD_EMPTY);
        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(MessagesException.EMAIL_INVALID);
        });
        When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
        {
            RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(8).WithMessage(MessagesException.SHORT_PASSWORD);
        });
    }
}