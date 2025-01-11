using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.TodoTask.Delete;

public class DeleteTaskTest : ToDoListClassFixture
{
    private const string METHOD = "task/delete";

    private readonly long _taskId;
    private readonly Guid _userIdentifier;

    public DeleteTaskTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _taskId = factory.GetTaskId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoDelete(METHOD, id, token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}