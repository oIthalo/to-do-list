using Bogus;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class UpdateUserRequestBuilder
{
    public static UpdateUserRequest Build()
    {
        return new Faker<UpdateUserRequest>()
            .RuleFor(x => x.Name, faker => faker.Person.FullName)
            .RuleFor(x => x.Email, faker => faker.Internet.Email());
    }
}
