using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Filters;

namespace ToDoList.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IsAuthAttribute : TypeFilterAttribute
{
    public IsAuthAttribute() : base(typeof(IsAuthFilter))
    {
    }
}