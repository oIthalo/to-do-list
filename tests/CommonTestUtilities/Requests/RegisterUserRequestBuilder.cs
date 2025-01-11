using Bogus;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RegisterUserRequestBuilder
{
    public static RegisterUserRequest Build(int passwordLength = 8)
    {
        return new Faker<RegisterUserRequest>()
            .RuleFor(x => x.Name, (faker) => (faker.Person.FirstName))
            .RuleFor(x => x.Email, (faker, user) => (faker.Internet.Email(user.Name)))
            .RuleFor(x => x.Password, (faker) => (faker.Internet.Password(passwordLength)));
    }
}