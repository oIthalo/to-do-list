using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.TodoTask.Create;

public class CreateTodoTaskTest : ToDoListClassFixture
{
    private const string METHOD = "task/create";

    private readonly Guid _userIdentifier;

    public CreateTodoTaskTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var request = CreateTaskRequestBuilder.Build();
        var token = AccessTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoPost(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}