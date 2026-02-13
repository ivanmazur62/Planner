using Planner.API.Middleware;

namespace Planner.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UsePlannerExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }
}