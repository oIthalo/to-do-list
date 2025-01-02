using FluentMigrator;

namespace ToDoList.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.TASKS_TABLE, "Adding table to get tasks")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Tasks")
            .WithColumn("Title").AsString(80).NotNullable()
            .WithColumn("Description").AsString(500).NotNullable()
            .WithColumn("Status").AsInt16().NotNullable();
    }
}