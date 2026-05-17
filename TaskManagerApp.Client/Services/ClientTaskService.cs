using System.Net.Http.Json;
using TaskManagerApp.Shared.Models;
using TaskManagerApp.Shared.Services;

namespace TaskManagerApp.Client.Services;

public class ClientTaskService : ITaskService
{
    private readonly HttpClient _httpClient;

    public ClientTaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TaskItem>>("api/tasks") ?? new List<TaskItem>();
    }

    public async Task AddTaskAsync(string title)
    {
        var task = new TaskItem { Title = title };
        await _httpClient.PostAsJsonAsync("api/tasks", task);
    }

    public async Task ToggleTaskAsync(Guid id)
    {
        await _httpClient.PutAsync($"api/tasks/{id}/toggle", null);
    }
}
