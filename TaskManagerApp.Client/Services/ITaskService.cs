using TaskManagerApp.Client.Models;

namespace TaskManagerApp.Client.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync();
    Task AddTaskAsync(string title);
    Task ToggleTaskAsync(Guid id);
}
