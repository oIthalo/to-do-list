using FluentMigrator;

namespace ToDoList.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.CASCADE_DELETION, "Cascade deletion on tasks and refresh tokens from users")]
public class Version0000004 : VersionBase
{
    public override void Up()
    {
        Delete.ForeignKey("FK_Users_Tasks_Identifier").OnTable("Tasks");
        Create.ForeignKey("FK_Users_Tasks_Identifier")
            .FromTable("Tasks").ForeignColumn("UserIdentifier")
            .ToTable("Users").PrimaryColumn("Identifier")
            .OnDelete(System.Data.Rule.Cascade);

        Delete.ForeignKey("FK_RefreshTokens_User_Id").OnTable("RefreshTokens");
        Create.ForeignKey("FK_RefreshTokens_User_Id")
            .FromTable("RefreshTokens").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);
    }
}