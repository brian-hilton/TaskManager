using TaskManager.Models;
using TaskManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSvelte", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>((c) =>
{
    c.UseSqlServer(ConnectionString);
});

var app = builder.Build();
app.UseCors("AllowSvelte");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints
app.MapGet("/getAllTasks", ([FromServices] AppDbContext db) =>
{
    var tasks = db.Set<TaskModel>().ToList();
    return tasks;
});

// Get all users
app.MapGet("/getAllUsers", ([FromServices] AppDbContext db) =>
{
    var users = db.Set<User>().ToList();
    return users;
});

// Get tasklist for user
app.MapGet("/users/{userId}/tasklists", async ([FromServices] AppDbContext db, int userId) =>
{
    var user = await db.Users
    .Include(u => u.TaskLists)
    .FirstOrDefaultAsync(u => u.Id == userId);

    if (user == null)
    {
        return Results.NotFound($"User with ID {userId} not found.");
    }

    return Results.Ok(user.TaskLists);
});

// Get tasks from tasklist for user
app.MapGet("/users/{userId}/taskLists/{taskListId}/tasks", async ([FromServices] AppDbContext db, int userId, int taskListId) =>
{
    var taskList = await db.TaskLists
        .Include(tl => tl.TaskModels)
        .FirstOrDefaultAsync(t1 => t1.Id == taskListId && t1.UserId == userId);

    if (taskList == null)
    {
        return Results.NotFound($"Tasklist with ID {taskListId} for User {userId} not found.");
    }

    return Results.Ok(taskList);

});

// Create task
app.MapPost("/postTask", ([FromServices] AppDbContext db, [FromBody] TaskModel model) =>
{
    db.Add(model);
    db.SaveChanges();
    return model;
});

// Create user
app.MapPost("/postUser", async ([FromServices] AppDbContext db, [FromBody] User user) =>
{
    if (user == null)
    {
        return Results.BadRequest("User data is required");
    }

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/postUser/{user.Id}", user);
});

// Create tasklist for user
app.MapPost("/users/{userId}/tasklists", async ([FromServices] AppDbContext db, int userId, TaskListDTO taskListDto) =>
{
    var taskList = new TaskList
    {
        Name = taskListDto.Name,
        Owner = taskListDto.Owner,
        CreatedDate = taskListDto.CreatedDate ?? DateTime.Now,
        UpdatedDate = taskListDto.UpdatedDate ?? DateTime.Now,
        UserId = userId,
        TaskModels = new List<TaskModel>()
    };

    var user = await db.Users.FindAsync(userId);
    if (user == null) 
    { 
        return Results.NotFound($"User with ID {userId} not found."); 
    }


    db.TaskLists.Add(taskList);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{userId}/tasklists/{taskList.Id}", taskList);


});

// Create task in user's tasklist
app.MapPost("/users/{userId}/taskLists/{taskListId}/tasks", async ([FromServices] AppDbContext db, int userId, int taskListId, [FromBody] TaskModelDTO taskModelDTO) =>
{
    if (taskModelDTO == null)
    {
        return Results.BadRequest("Task data is required.");
    }

    var user = await db.Users.Include(u => u.TaskLists).FirstOrDefaultAsync(u => u.Id == userId);
    if (user == null)
    {
        return Results.NotFound("User not found.");
    }

    var taskList = user.TaskLists?.FirstOrDefault(t => t.Id == taskListId);
    if (taskList == null)
    {
        return Results.NotFound("Tasklist not found.");
    }

    var taskModel = new TaskModel
    {
        Title = taskModelDTO.Title,
        Status = taskModelDTO.Status,
        Description = taskModelDTO.Description,
        CreatedDate = DateTime.UtcNow,
        UpdatedDate = DateTime.UtcNow,
        TaskListId = taskListId
    };

    db.TaskModels.Add(taskModel);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{userId}/tasklists/{taskListId}/tasks/{taskModel.Id}", taskModel);
});


app.Run();


