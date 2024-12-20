using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        
        await _userRepository.Add(user);
        await _unitOfWork.Commit();

        return new RegisterUserResponse()
        {
            Name = user.Name,
        };
    }

    private static void Validate(RegisterUserRequest request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}