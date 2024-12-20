using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList.Communication.Responses;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ToDoListException)
            HandleProjectException(context);
        else
            ThrowExceptionUnknown(context);
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException errorOnValidationException)
        {
            var exception = context.Exception as ErrorOnValidationException;

            context.HttpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(new ErrorsResponse(exception!.ErrorMessages));
        }
    }

    private static void ThrowExceptionUnknown(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(MessagesException.UNKNOWN_ERROR);
    }
}