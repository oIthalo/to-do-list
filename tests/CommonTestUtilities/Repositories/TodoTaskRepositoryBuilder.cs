using CommonTestUtilities.Entities;
using Moq;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;

namespace CommonTestUtilities.Repositories;

public class TodoTaskRepositoryBuilder
{
    private readonly Mock<ITodoTaskRepository> _repository;

    public TodoTaskRepositoryBuilder() => _repository = new Mock<ITodoTaskRepository>();

    public ITodoTaskRepository Build() => _repository.Object;

    public void GetById(User user, TodoTask todoTask)
    {
        _repository.Setup(repository => repository.GetById(user, todoTask.Id)).ReturnsAsync(todoTask);
    }

    public void GetAllTaskUsers(User user)
    {
        var tasks = new List<TodoTask>()
        {
            TodoTaskBuilder.Build(user),
            TodoTaskBuilder.Build(user),
        };

        _repository.Setup(repository => repository.GetAllUserTasks(user))
                   .ReturnsAsync(tasks);
    }
}