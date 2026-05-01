using TaskManagerApp.Client.Models;

namespace TaskManagerApp.Client.Services;

public class MockTaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new()
    {
        new TaskItem { Title = "Leer el artículo 1", IsCompleted = true },
        new TaskItem { Title = "Aprender sobre Render Modes" },
        new TaskItem { Title = "Implementar app con Interactive Auto" }
    };

    public Task<List<TaskItem>> GetTasksAsync()
    {
        return Task.FromResult(_tasks.ToList());
    }

    public Task AddTaskAsync(string title)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            _tasks.Add(new TaskItem { Title = title });
        }
        return Task.CompletedTask;
    }

    public Task ToggleTaskAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
        }
        return Task.CompletedTask;
    }
}
