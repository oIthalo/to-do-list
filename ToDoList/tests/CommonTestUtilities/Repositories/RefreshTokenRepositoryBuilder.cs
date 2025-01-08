using Moq;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public class RefreshTokenRepositoryBuilder
{
    private readonly Mock<IRefreshTokenRepository> _repository;

    public RefreshTokenRepositoryBuilder() => _repository = new Mock<IRefreshTokenRepository>();

    public IRefreshTokenRepository Build() => _repository.Object;

    public void Get(RefreshToken refreshToken)
    {
        _repository.Setup(x => x.Get(refreshToken.Value)).ReturnsAsync(refreshToken);
    }
}