using ToDoList.Communication.Requests;

namespace ToDoList.Application.UseCases.User.Password.Change;

public interface IPasswordChangeUseCase
{
    Task Execute(PasswordChangeRequest request);
}