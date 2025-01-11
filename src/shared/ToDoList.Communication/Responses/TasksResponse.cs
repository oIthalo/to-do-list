namespace ToDoList.Communication.Responses;

public class TasksResponse
{
    public IList<TaskResponse> Tasks { get; set; } = [];
}