using TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<testContext>((c) =>
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
app.MapGet("/getAllTasks", ([FromServices] testContext db) =>
{
    var tasks = db.Set<TaskModel>().ToList();
    return tasks;
});

app.MapPost("/postTask", ([FromServices] testContext db, [FromBody] TaskModel model) =>
{
    db.Add(model);
    db.SaveChanges();
    return model;
});

app.Run();


