﻿using FluentMigrator;

namespace ToDoList.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.REFRESH_TOKENS_TABLE, "Table to get the users refresh tokens")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        CreateTable("RefreshTokens")
            .WithColumn("Value").AsString().NotNullable()
            .WithColumn("UserIdentifier").AsGuid().NotNullable().ForeignKey("FK_RefreshTokens_User_Identifier", "Users", "Identifier");
    }
}