using FluentValidation.Results;
using ToDoList.Communication.Requests;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Criptography;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Exception;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.User.Password.Change;

public class PasswordChangeUseCase : IPasswordChangeUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;

    public PasswordChangeUseCase(
        IUserRepository userRepository,
        ILoggedUser loggedUser,
        IPasswordEncripter passwordEncripter,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _loggedUser = loggedUser;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(PasswordChangeRequest request)
    {
        var user = await _loggedUser.User();

        Validate(user, request);

        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _userRepository.Update(user);
        await _unitOfWork.Commit();
    }

    private void Validate(Domain.Entities.User user, PasswordChangeRequest request)
    {
        var result = new PasswordChangeValidator().Validate(request);

        if (!_passwordEncripter.IsValid(request.Password, user.Password))
            result.Errors.Add(new ValidationFailure(string.Empty, MessagesException.INVALID_PASSWORD))
;
        if (_passwordEncripter.IsValid(request.NewPassword, user.Password))
            result.Errors.Add(new ValidationFailure(string.Empty, MessagesException.PASSWORD_ALREADY_USED));

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}