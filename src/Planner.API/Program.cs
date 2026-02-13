using Planner.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

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
builder.Services.AddPlannerHealthChecks(builder.Configuration);
builder.Services.AddPlannerCors(builder.Configuration);

var app = builder.Build();

app.UsePlannerExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Planner");
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();