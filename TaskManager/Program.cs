using TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>((c) =>
{
    c.UseSqlServer(ConnectionString);
});

var app = builder.Build();

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

app.MapGet("/getAllUsers", ([FromServices] AppDbContext db) =>
{
    var users = db.Set<User>().ToList();
    return users;
});

app.MapPost("/postTask", ([FromServices] AppDbContext db, [FromBody] TaskModel model) =>
{
    db.Add(model);
    db.SaveChanges();
    return model;
});

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

app.Run();


