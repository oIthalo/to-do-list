namespace ToDoList.Infrastructure.Migrations;

public abstract class DataBaseVersions
{
    public const int USER_TABLE = 1;
    public const int TASKS_TABLE = 2;
    public const int REFRESH_TOKENS_TABLE = 3;
    public const int CASCADE_DELETION = 4;
}