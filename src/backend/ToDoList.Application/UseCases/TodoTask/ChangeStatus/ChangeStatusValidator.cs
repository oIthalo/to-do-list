using FluentValidation;
using ToDoList.Communication.Requests;
using ToDoList.Exception;

namespace ToDoList.Application.UseCases.TodoTask.ChangeStatus;

public class ChangeStatusValidator : AbstractValidator<ChangeTaskStatusRequest>
{
    public ChangeStatusValidator()
    {
        RuleFor(x => x.Status).IsInEnum().WithMessage(MessagesException.STATUS_INVALID);
    }
}