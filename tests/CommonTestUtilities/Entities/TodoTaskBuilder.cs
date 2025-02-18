using Bogus;
using ToDoList.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class TodoTaskBuilder
{
    public static TodoTask Build(User user)
    {
        return new Faker<TodoTask>()
            .RuleFor(x => x.Title, (f) => (f.Lorem.Word()))
            .RuleFor(x => x.Description, (f) => (f.Lorem.Paragraph()))
            .RuleFor(x => x.Done, (f) => false)
            .RuleFor(x => x.UserIdentifier, (f) => user.Identifier);

    }
}