using ToDoList.Communication.Requests;

namespace ToDoList.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(UpdateUserRequest request);
}