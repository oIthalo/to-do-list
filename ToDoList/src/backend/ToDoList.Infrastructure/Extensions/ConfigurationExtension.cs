using Microsoft.Extensions.Configuration;

namespace ToDoList.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static string ConnectionStringBuilder(this IConfiguration configuration) => configuration.GetConnectionString("Connection")!;

    public static bool IsUnitTestEnviroment(this IConfiguration configuration) => configuration.GetValue<bool>("InMemoryTest");
}