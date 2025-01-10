using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebAPI.Test.TodoTask.GetById;

public class GetTaskByIdTest : ToDoListClassFixture
{
    private const string METHOD = "task/get";

    private readonly long _taskId;
    private readonly Guid _userIdentifier;

    public GetTaskByIdTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _taskId = factory.GetTaskId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoGet(METHOD, token, id:id);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("title").GetString().Should().NotBeNullOrWhiteSpace();
    }
}