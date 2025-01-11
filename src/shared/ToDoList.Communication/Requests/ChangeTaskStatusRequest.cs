using ToDoList.Communication.Enums;

namespace ToDoList.Communication.Requests;

public class ChangeTaskStatusRequest
{
    public ETaskStatus Status { get; set; }
}