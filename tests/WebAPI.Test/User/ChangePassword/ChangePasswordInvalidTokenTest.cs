using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using ToDoList.Exception;

namespace WebAPI.Test.User.ChangePassword;

public class ChangePasswordInvalidTokenTest : ToDoListClassFixture
{
    private const string METHOD = "user/change-password";

    public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_No_Token()
    {
        var request = PasswordChangeRequestBuilder.Build();

        var response = await DoPut(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.NO_TOKEN);
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var request = PasswordChangeRequestBuilder.Build();
        var token = AccessTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var response = await DoPut(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("errorMessages")[0].GetString().Should().Be(MessagesException.USER_WITHOUT_PERMISSION);
    }
}