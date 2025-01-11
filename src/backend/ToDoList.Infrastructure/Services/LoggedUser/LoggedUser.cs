using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Infrastructure.DataAccess;

namespace ToDoList.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly ToDoListDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;


    public LoggedUser(
        ToDoListDbContext dbContext,
        ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var userIdentifier = Guid.Parse(jwtSecurityToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value);

        return await _dbContext.Users.AsNoTracking().FirstAsync(x => x.Active && x.Identifier.Equals(userIdentifier));
    }
}