using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using ToDoList.Exception;

namespace WebAPI.Test.TodoTask.ChangeStatus;

public class ChangeTaskStatusInvalidTokenTest : ToDoListClassFixture
{
    private const string METHOD = "task/change-status";

    private readonly long _taskId;

    public ChangeTaskStatusInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _taskId = factory.GetTaskId();
    }

    [Fact]
    public async Task Error_No_Token()
    {
        var request = ChangeTaskStatusRequestBuilder.Build();

        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoPut(METHOD, request, id: id);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.NO_TOKEN);
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var request = ChangeTaskStatusRequestBuilder.Build();
        var token = AccessTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoPut(METHOD, request, token: token, id: id);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.USER_WITHOUT_PERMISSION);
    }
}