using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> User();
}