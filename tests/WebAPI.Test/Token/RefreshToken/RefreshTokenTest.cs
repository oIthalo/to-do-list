using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using ToDoList.Domain.Entities;

namespace WebAPI.Test.Token.RefreshToken;

public class RefreshTokenTest : ToDoListClassFixture
{
    private const string METHOD = "token/refresh-token";

    private readonly ToDoList.Domain.Entities.RefreshToken _refreshToken;

    public RefreshTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _refreshToken = factory.GetRefreshToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = NewTokenRequestBuilder.Build();
        request.RefreshToken = _refreshToken.Value;

        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}