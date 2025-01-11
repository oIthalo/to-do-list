using Bogus;
using ToDoList.Communication.Enums;
using ToDoList.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class ChangeTaskStatusRequestBuilder
{
    public static ChangeTaskStatusRequest Build()
    {
        return new Faker<ChangeTaskStatusRequest>()
            .RuleFor(x => x.Status, (f) => (f.PickRandom<ETaskStatus>()));
    }
}