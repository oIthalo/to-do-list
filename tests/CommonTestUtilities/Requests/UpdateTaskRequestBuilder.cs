using Bogus;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class UpdateTaskRequestBuilder
{
    public static UpdateTaskRequest Build()
    {
        return new Faker<UpdateTaskRequest>()
            .RuleFor(x => x.Title, faker => faker.Lorem.Sentence(3))
            .RuleFor(x => x.Description, faker => faker.Lorem.Paragraph());
    }
}
