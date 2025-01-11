using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.User.ChangePassword;

public class ChangePasswordTest : ToDoListClassFixture
{
    private const string METHOD = "user/change-password";

    private readonly Guid _userIdentifier;
    private readonly string _password;

    public ChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = PasswordChangeRequestBuilder.Build();
        request.Password = _password;

        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPut(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}