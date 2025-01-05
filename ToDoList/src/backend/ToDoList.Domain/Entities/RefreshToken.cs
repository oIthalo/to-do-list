namespace ToDoList.Domain.Entities;

public class RefreshToken : EntityBase
{
    public required string Value { get; set; } = string.Empty;
    public required Guid UserIdentifier { get; set; }

    public User User { get; set; } = default!;
}