using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByIdentifer(Guid identifier);
    void Update(User user);
    Task<bool> ExistActiveUserWithEmail(string email);
    void Delete(User user);
}