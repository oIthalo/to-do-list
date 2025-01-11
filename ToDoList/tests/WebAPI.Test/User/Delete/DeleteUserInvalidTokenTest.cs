using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using ToDoList.Exception;

namespace WebAPI.Test.User.Delete;

public class DeleteUserInvalidTokenTest : ToDoListClassFixture
{
    private const string METHOD = "user/delete";

    private readonly long _taskId;

    public DeleteUserInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _taskId = factory.GetTaskId();
    }

    [Fact]
    public async Task Error_No_Token()
    {
        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoDelete(METHOD);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.NO_TOKEN);
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var token = AccessTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoDelete(METHOD, token:token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.USER_WITHOUT_PERMISSION);
    }
}