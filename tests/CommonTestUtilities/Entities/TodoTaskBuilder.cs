using Bogus;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;

namespace CommonTestUtilities.Entities;

public class TodoTaskBuilder
{
    public static TodoTask Build(User user)
    {
        return new Faker<TodoTask>()
            .RuleFor(x => x.Title, (f) => (f.Lorem.Word()))
            .RuleFor(x => x.Description, (f) => (f.Lorem.Paragraph()))
            .RuleFor(x => x.Status, (f) => (f.PickRandom<EStatusTask>()))
            .RuleFor(x => x.UserIdentifier, (f) => user.Identifier);

    }
}