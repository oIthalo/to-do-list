using AutoMapper;
using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Criptography;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Exception.ExceptionsBase;
using ToDoList.Infrastructure.DataAccess;
using ToDoList.Infrastructure.Security.Tokens.Refresh.Generator;

namespace ToDoList.Application.UseCases.Login.DoLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUseCase(
        IUserRepository userRepository,
        IMapper mapper,
        IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncripter passwordEncripter,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserResponse> Execute(DoLoginRequest request)
    {
        Validate(request);

        var user = await _userRepository.GetByEmail(request.Email);

        // user is null or password is false
        if (user is null || !_passwordEncripter.IsValid(request.Password, user.Password))
            throw new ErrorOnInvalidLogin();

        var token = _accessTokenGenerator.Generate(user.Identifier);

        var refreshToken = await CreateAndSaveRefreshToken(user);

        return new RegisterUserResponse()
        {
            Name = user.Name,
            Tokens = new TokensResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
            }
        };
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = new Domain.Entities.RefreshToken
        {
            Value = _refreshTokenGenerator.Generate(),
            UserId = user.Id
        };

        await _refreshTokenRepository.SaveNewRefreshToken(refreshToken);

        await _unitOfWork.Commit();

        return refreshToken.Value;
    }

    private static void Validate(DoLoginRequest request)
    {
        var result = new LoginValidator().Validate(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}