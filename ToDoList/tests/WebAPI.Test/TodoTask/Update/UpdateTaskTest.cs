using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.TodoTask.Delete;

public class UpdateTaskTest : ToDoListClassFixture
{
    private const string METHOD = "task/update";

    private readonly long _taskId;
    private readonly Guid _userIdentifier;

    public UpdateTaskTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _taskId = factory.GetTaskId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var request = UpdateTaskRequestBuilder.Build();
        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoPut(METHOD, request, token, id);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}