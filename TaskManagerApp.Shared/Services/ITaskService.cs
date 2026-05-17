using TaskManagerApp.Shared.Models;

namespace TaskManagerApp.Shared.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync();
    Task AddTaskAsync(string title);
    Task ToggleTaskAsync(Guid id);
}
