using FluentValidation;
using ToDoList.Communication.Requests;
using ToDoList.Exception;

namespace ToDoList.Application.UseCases.User.Password.Change;

public class PasswordChangeValidator : AbstractValidator<PasswordChangeRequest>
{
    public PasswordChangeValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage(MessagesException.INVALID_PASSWORD);
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(MessagesException.PASSWORD_EMPTY);

        When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
        {
            RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(8).WithMessage(MessagesException.INVALID_PASSWORD);
        });

        When(x => !string.IsNullOrWhiteSpace(x.NewPassword), () =>
        {
            RuleFor(x => x.NewPassword.Length).GreaterThanOrEqualTo(8).WithMessage(MessagesException.SHORT_PASSWORD);
        });
    }
}