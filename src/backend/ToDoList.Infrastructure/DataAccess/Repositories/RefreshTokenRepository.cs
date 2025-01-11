using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.DataAccess.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ToDoListDbContext _dbContext;

    public RefreshTokenRepository(ToDoListDbContext dbContext) => _dbContext = dbContext;

    public async Task<RefreshToken?> Get(string refreshToken) =>
        await _dbContext.RefreshTokens.AsNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.Value.Equals(refreshToken));

    public async Task SaveNewRefreshToken(RefreshToken refreshToken)
    {
        var tokens = _dbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

        _dbContext.RemoveRange(tokens);

        await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }
}