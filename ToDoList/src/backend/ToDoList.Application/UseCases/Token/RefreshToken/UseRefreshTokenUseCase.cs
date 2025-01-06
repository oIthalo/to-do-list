using ToDoList.Communication.Requests;
using ToDoList.Communication.Responses;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Domain.ValueObjects;
using ToDoList.Exception.ExceptionsBase;

namespace ToDoList.Application.UseCases.Token.RefreshToken;

public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
{
    private readonly IRefreshTokenRepository _tokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public UseRefreshTokenUseCase(
        IRefreshTokenRepository tokenRepository, 
        IUnitOfWork unitOfWork, 
        IAccessTokenGenerator accessTokenGenerator, 
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _tokenRepository = tokenRepository;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<TokensResponse> Execute(NewTokenRequest request)
    {
        var refreshToken = await _tokenRepository.Get(request.RefreshToken) ?? throw new RefreshTokenNotFound();

        var refreshTokenValidUntil = refreshToken.CreatedOn.AddDays(ToDoListRuleConstants.REFRESH_TOKEN_EXPIRATION_DAYS);
        if (DateTime.Compare(refreshTokenValidUntil, DateTime.UtcNow) < 0)
            throw new RefreshTokenExpiredException();

        var newToken = new Domain.Entities.RefreshToken()
        {
            Value = _refreshTokenGenerator.Generate(),
            UserId = refreshToken.UserId,
        };

        await _tokenRepository.SaveNewRefreshToken(newToken);
        await _unitOfWork.Commit();

        return new TokensResponse()
        {
            AccessToken = _accessTokenGenerator.Generate(refreshToken.User.Identifier),
            RefreshToken = newToken.Value,
        };
    }
}