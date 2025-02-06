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
        if (context.Exception is ToDoListException toDoListException)
            HandleProjectException(toDoListException, context);
        else
            ThrowExceptionUnknown(context);

    }

    private static void HandleProjectException(ToDoListException toDoListException,ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)toDoListException.GetStatusCode();
        context.Result = new ObjectResult(new ErrorResponse(toDoListException.GetErrorMessages()));
    }

    private static void ThrowExceptionUnknown(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(MessagesException.UNKNOWN_ERROR);
    }
}
