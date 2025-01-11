using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.User.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(
        IUserRepository userRepository, 
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var loggedUser = await _loggedUser.User();
        var user = await _userRepository.GetByIdentifer(loggedUser.Identifier)

        _userRepository.Delete(user);
        await _unitOfWork.Commit();
    }
}