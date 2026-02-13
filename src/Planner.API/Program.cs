using Planner.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddPlannerDataProtection();
builder.Services.AddPlannerDbContext(builder.Configuration);
builder.Services.AddPlannerRepositories();
builder.Services.AddPlannerServices();
builder.Services.AddPlannerIdentity();
builder.Services.AddPlannerAuthentication(builder.Configuration);
builder.Services.AddPlannerSwagger();
builder.Services.AddPlannerValidation();
builder.Services.AddPlannerControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();