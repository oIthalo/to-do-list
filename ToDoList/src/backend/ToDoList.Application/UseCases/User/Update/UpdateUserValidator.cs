using FluentValidation;
using ToDoList.Communication.Requests;
using ToDoList.Exception;

namespace ToDoList.Application.UseCases.User.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(MessagesException.USER_NAME_EMPTY);
        RuleFor(x => x.Email).NotEmpty().WithMessage(MessagesException.EMAIL_EMPTY);
        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(MessagesException.EMAIL_INVALID);
        });
    }
}