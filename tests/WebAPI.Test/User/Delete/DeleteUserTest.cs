using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.User.Delete;

public class DeleteUserTest : ToDoListClassFixture
{
    private const string METHOD = "user/delete";

    private readonly long _taskId;
    private readonly Guid _userIdentifier;

    public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _taskId = factory.GetTaskId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var id = IdEncripterBuilder.Build().Encode(_taskId);

        var response = await DoDelete(METHOD, token:token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}