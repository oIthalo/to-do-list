﻿namespace ToDoList.Communication.Requests;

public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}