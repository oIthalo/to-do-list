using Microsoft.Extensions.Configuration;

namespace ToDoList.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static string ConnectionStringBuilder(this IConfiguration configuration) => configuration.GetConnectionString("Connection")!;
}