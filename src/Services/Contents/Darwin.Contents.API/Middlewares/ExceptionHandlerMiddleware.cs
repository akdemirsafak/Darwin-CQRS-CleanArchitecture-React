using Darwin.Shared.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Darwin.Contents.API.Middlewares
{
    public static class ExceptionHandler
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ValidationException => StatusCodes.Status422UnprocessableEntity,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var response = DarwinResponse<NoContentDto>.Fail(exceptionFeature.Error.Message, statusCode);
                    await context.Response.WriteAsJsonAsync(response);
                });
            });
        }
    }
}
