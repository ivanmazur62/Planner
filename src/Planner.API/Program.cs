using Microsoft.EntityFrameworkCore;
using Planner.Core.Interfaces;
using Planner.Infrastructure.Data;
using Planner.Infrastructure.Repositories;
using Planner.Services;
using Planner.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IPlannerTaskRepository, PlannerTaskRepository>();
builder.Services.AddScoped<IPlannerTaskService, PlannerTaskService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();