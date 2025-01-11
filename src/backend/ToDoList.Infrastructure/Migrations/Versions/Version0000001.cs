using FluentMigrator;

namespace ToDoList.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.USER_TABLE, "The first version of database")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Email").AsString(160).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("Identifier").AsGuid().NotNullable().Unique();
    }
}