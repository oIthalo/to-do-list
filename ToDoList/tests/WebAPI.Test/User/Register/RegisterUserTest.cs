using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using ToDoList.Exception;

namespace WebAPI.Test.User.Register;

public class RegisterUserTest : ToDoListClassFixture
{
    private const string METHOD = "user/register";

    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("tokens").GetProperty("refreshToken").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var error = responseData.RootElement.GetProperty("errorMessages")[0].GetString();

        error.Should().Be(MessagesException.USER_NAME_EMPTY);
    }
}