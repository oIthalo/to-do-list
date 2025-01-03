using ToDoList.Communication.Enums;

namespace ToDoList.Communication.Responses;

public class TaskResponse
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public ETaskStatus Status { get; set; }
}