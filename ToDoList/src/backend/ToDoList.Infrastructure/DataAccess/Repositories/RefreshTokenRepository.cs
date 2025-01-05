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
}