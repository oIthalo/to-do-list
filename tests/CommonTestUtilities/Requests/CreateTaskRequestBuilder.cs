using Bogus;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class CreateTaskRequestBuilder
{
    public static CreateTaskRequest Build()
    {
        return new Faker<CreateTaskRequest>()
            .RuleFor(x => x.Title, faker => faker.Lorem.Sentence(3))
            .RuleFor(x => x.Description, faker => faker.Lorem.Paragraph());
    }
}