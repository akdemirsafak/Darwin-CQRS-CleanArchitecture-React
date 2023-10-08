using Darwin.Core.BaseDto;
using Microsoft.AspNetCore.Diagnostics;

namespace Darwin.API.Middlewares;

public static class CustomExceptionHandlerMiddleware
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionFeature.Error;
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(DarwinResponse<object>.Fail(exception.Message));
            });
        });
    }
}
