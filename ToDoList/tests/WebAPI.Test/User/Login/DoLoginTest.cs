using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json; 
using ToDoList.Exception;

namespace WebAPI.Test.User.Login;

public class DoLoginTest : ToDoListClassFixture
{
    private const string METHOD = "user/login";

    private readonly ToDoList.Domain.Entities.User _user;
    private readonly string _email;
    private readonly string _password;

    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _user = factory.GetUser();
        _email = factory.GetEmail();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = LoginRequestBuilder.Build();
        request.Email = _email;
        request.Password = _password;

        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_user.Name);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("tokens").GetProperty("refreshToken").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Email_Empty()
    {
        var request = LoginRequestBuilder.Build();
        request.Email = string.Empty;

        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var error = responseData.RootElement.GetProperty("errorMessages")[0].GetString();

        error.Should().Be(MessagesException.EMAIL_EMPTY);
    }
}