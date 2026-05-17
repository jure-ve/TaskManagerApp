using TaskManagerApp.Client.Pages;
using TaskManagerApp.Components;
using TaskManagerApp.Shared.Services;
using TaskManagerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<ITaskService, ServerTaskService>();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TaskManagerApp.Client._Imports).Assembly);

// Minimal APIs para el cliente WebAssembly
app.MapGet("/api/tasks", async (ITaskService taskService) => await taskService.GetTasksAsync());
app.MapPost("/api/tasks", async (TaskManagerApp.Shared.Models.TaskItem task, ITaskService taskService) => {
    await taskService.AddTaskAsync(task.Title);
    return Results.Ok();
});
app.MapPut("/api/tasks/{id}/toggle", async (Guid id, ITaskService taskService) => {
    await taskService.ToggleTaskAsync(id);
    return Results.Ok();
});

app.MapPost("/api/login", async (HttpContext context) => {
    var form = await context.Request.ReadFormAsync();
    var username = form["username"].ToString() ?? "DemoUser";
    
    var claims = new List<System.Security.Claims.Claim> { 
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username) 
    };
    var identity = new System.Security.Claims.ClaimsIdentity(claims, "Cookies");
    await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignInAsync(context, "Cookies", new System.Security.Claims.ClaimsPrincipal(identity));
    
    return Results.Redirect("/");
});

app.MapPost("/api/logout", async (HttpContext context) => {
    await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(context, "Cookies");
    return Results.Redirect("/");
});

app.Run();
