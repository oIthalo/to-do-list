using CommonTestUtilities.Entities;
using Moq;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public class UserRepositoryBuilder
{
    private readonly Mock<IUserRepository> _repository;

    public UserRepositoryBuilder() => _repository = new Mock<IUserRepository>();

    public IUserRepository Build() => _repository.Object;

    public void ExistActiveUserWithEmail(string email)
    {
        _repository.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public void GetByEmail(User user)
    {
        _repository.Setup(repository => repository.GetByEmail(user.Email)).ReturnsAsync(user);
    }
}