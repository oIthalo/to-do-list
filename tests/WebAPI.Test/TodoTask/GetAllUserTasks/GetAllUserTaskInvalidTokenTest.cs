using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using ToDoList.Exception;

namespace WebAPI.Test.TodoTask.GetAllUserTasks;

public class GetAllUserTaskInvalidTokenTest : ToDoListClassFixture
{
    private const string METHOD = "task/get-all";

    
    public GetAllUserTaskInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    { 
    }

    [Fact]
    public async Task Error_No_Token()
    {
        var response = await DoGet(METHOD);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.NO_TOKEN);
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var token = AccessTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var response = await DoGet(METHOD, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.USER_WITHOUT_PERMISSION);
    }
}