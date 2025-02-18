namespace ToDoList.Communication.Requests;

public class ChangeStatusRequest 
{
    public bool Done { get; set; } = false;
}