using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using ToDoList.Exception;

namespace WebAPI.Test.TodoTask.Udate;

public class UpdateTaskInvalidTokenTest : ToDoListClassFixture
{
    private const string METHOD = "task/update";

    private readonly long _taskId;

    public UpdateTaskInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _taskId = factory.GetTaskId();
    }

    [Fact]
    public async Task Error_No_Token()
    {
        var request = UpdateTaskRequestBuilder.Build();

        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoPutWithId(METHOD, id,  request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.NO_TOKEN);
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var request = UpdateTaskRequestBuilder.Build();
        var token = AccessTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoPutWithId(METHOD, id, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.USER_WITHOUT_PERMISSION);
    }
}