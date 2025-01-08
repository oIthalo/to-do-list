using ToDoList.Communication.Requests;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.User.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(
        IUserRepository userRepository,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateUserRequest request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var user = await _userRepository.GetByIdentifer(loggedUser.Identifier);
        user!.Name = request.Name;
        user.Email = request.Email;

        _userRepository.Update(user);
        await _unitOfWork.Commit();
    }

    private static void Validate(UpdateUserRequest request)
    {
        var result = new UpdateUserValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}