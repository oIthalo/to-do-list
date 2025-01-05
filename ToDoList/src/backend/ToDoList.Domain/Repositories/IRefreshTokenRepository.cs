using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> Get(string refreshToken);
}