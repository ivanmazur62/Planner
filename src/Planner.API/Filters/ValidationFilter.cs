using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Planner.API.Filters;

public sealed class ValidationFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if(argument == null) continue;
            
            var validationType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = serviceProvider.GetService(validationType) as IValidator;
            
            if(validator == null) continue;
            
            var result = await validator.ValidateAsync(
                new ValidationContext<object>(argument),
                context.HttpContext.RequestAborted);
            
            if(result.IsValid) continue;

            context.Result = new BadRequestObjectResult(new
            {
                errors = result.Errors.Select(e => new {e.PropertyName, e.ErrorMessage})
            });
            return;
        }
        
        await next();
    }
}