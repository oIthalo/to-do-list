using CommonTestUtilities.AutoMapper;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using FluentAssertions;
using ToDoList.Application.UseCases.User.Profile;

namespace UseCases.Test.User.Profile;

public class GetUserProfileUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Name.Should().NotBeNull().And.Be(user.Name);
        result.Email.Should().NotBeNull().And.Be(user.Email);
    }

    private static GetUserProfileUseCase CreateUseCase(ToDoList.Domain.Entities.User? user = default)
    {
        var loggedUser = LoggedUserBuilder.Build(user!);
        var autoMapper = AutoMapperBuilder.Build();

        return new GetUserProfileUseCase(loggedUser, autoMapper);
    }
}