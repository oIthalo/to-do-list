using Bogus;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class LoginRequestBuilder
{
    public static DoLoginRequest Build(int passwordLength = 8)
    {
        return new Faker<DoLoginRequest>()
            .RuleFor(x => x.Email, (faker) => (faker.Internet.Email()))
            .RuleFor(x => x.Password, (faker) => (faker.Internet.Password(passwordLength)));
    }
}