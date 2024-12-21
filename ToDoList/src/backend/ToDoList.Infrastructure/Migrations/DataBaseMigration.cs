using Dapper;
using Microsoft.Data.SqlClient;

namespace ToDoList.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public static void Migrate(string connectionString)
    {
        EnsureDataBase(connectionString);
    }

    private static void EnsureDataBase(string connectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.Remove("Database");

        using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        var records = dbConnection.Query("SELECT * FROM sys.databases WHERE = @name", parameters);

        if (!records.Any())
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
    }
}