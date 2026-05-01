using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskManagerApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<ITaskService, MockTaskService>();

await builder.Build().RunAsync();
