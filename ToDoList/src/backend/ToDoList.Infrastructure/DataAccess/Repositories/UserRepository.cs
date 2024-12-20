using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ToDoListDbContext _dbContext;

    public UserRepository(ToDoListDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<User?> GetByEmailAndPassword(string email, string password) => await _dbContext.Users.FirstOrDefaultAsync(x => x.Active && x.Email.Equals(email) && x.Password.Equals(password));
}