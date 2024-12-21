using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Criptography;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.Login.DoLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;

    public LoginUseCase(
        IUserRepository userRepository,
        IMapper mapper,
        IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncripter passwordEncripter)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<RegisterUserResponse> Execute(DoLoginRequest request)
    {
        Validate(request);

        var passwordEncripted = _passwordEncripter.Encrypt(request.Password);
        var user = await _userRepository.GetByEmailAndPassword(request.Email, passwordEncripted) ?? throw new ErrorOnInvalidLogin();

        var token = _accessTokenGenerator.Generate(user.Identifier);

        return new RegisterUserResponse()
        {
            Name = user.Name,
            Tokens = new TokensResponse()
            {
                AccessToken = token
            }
        };
    }

    private void Validate(DoLoginRequest request)
    {
        var result = new LoginValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}