using Bogus;
using CommonTestUtilities.Tokens;
using ToDoList.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class RefreshTokenBuilder
{
    public static RefreshToken Build(User user)
    {
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();

        var refreshToken = new Faker<RefreshToken>()
            .RuleFor(x => x.Value, (f) => refreshTokenGenerator.Generate())
            .RuleFor(x => x.UserId, (f) => user.Id)
            .RuleFor(x => x.User, (f) => user);

        return refreshToken;
    }
}