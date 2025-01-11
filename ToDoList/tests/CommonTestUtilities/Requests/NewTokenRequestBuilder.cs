using Bogus;
using CommonTestUtilities.Tokens;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class NewTokenRequestBuilder
{
    public static NewTokenRequest Build()
    {
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();

        return new Faker<NewTokenRequest>()
            .RuleFor(x => x.RefreshToken, (f) => refreshTokenGenerator.Generate());
    }
}