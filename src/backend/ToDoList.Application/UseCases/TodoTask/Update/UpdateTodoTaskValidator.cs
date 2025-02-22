﻿using FluentValidation;
using ToDoList.Communication.Requests;
using ToDoList.Exception;

namespace ToDoList.Application.UseCases.TodoTask.Update;

public class UpdateTodoTaskValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTodoTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(MessagesException.TITLE_EMPTY);
        RuleFor(x => x.Description).NotEmpty().WithMessage(MessagesException.DESCRIPTION_EMPTY);
        When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
        {
            RuleFor(x => x.Description.Length).LessThanOrEqualTo(500).WithMessage(MessagesException.DESCRIPTION_INVALID);
        });
    }
}