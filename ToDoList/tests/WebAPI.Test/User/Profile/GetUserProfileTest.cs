using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebAPI.Test.User.Profile;

public class GetUserProfileTest : ToDoListClassFixture
{
    private const string METHOD = "user/profile";

    private readonly ToDoList.Domain.Entities.User _user = default!;
    private readonly Guid _userIdentifier;

    public GetUserProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _user = factory.GetUser();
    }

    [Fact]
    public async Task Success()
    {
        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet(METHOD, token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_user.Name);
    }
}