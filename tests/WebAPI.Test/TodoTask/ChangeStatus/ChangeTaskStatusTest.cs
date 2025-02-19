using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.TodoTask.ChangeStatus;

public class ChangeTaskStatusTest : ToDoListClassFixture
{
    private const string METHOD = "task/change-status";

    private readonly Guid _userIdentifier;
    private readonly long _taskId;

    public ChangeTaskStatusTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _taskId = factory.GetTaskId();
    }

    [Fact]
    public async Task Success()
    {
        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        
        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoPut(METHOD, false, token:token, id:id);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}