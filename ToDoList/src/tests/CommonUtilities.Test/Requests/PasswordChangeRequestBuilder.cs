using Bogus;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class PasswordChangeRequestBuilder
{
    public static PasswordChangeRequest Build(int passwordLength = 8, int newPasswordLength = 8)
    {
        return new Faker<PasswordChangeRequest>()
            .RuleFor(x => x.Password, (faker) => faker.Internet.Password(passwordLength))
            .RuleFor(x => x.NewPassword, (faker) => faker.Internet.Password(newPasswordLength));
    }
}