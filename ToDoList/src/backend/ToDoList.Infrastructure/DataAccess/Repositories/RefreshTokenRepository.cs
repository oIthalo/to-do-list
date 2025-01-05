using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.DataAccess.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ToDoListDbContext _dbContext;

    public RefreshTokenRepository(ToDoListDbContext dbContext) => _dbContext = dbContext;
}