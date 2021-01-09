using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ToDoListDataService.Utils;

namespace ToDoListDataService.Exceptions
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        if (contextFeature != null)
                        {
                            if (contextFeature.Error is TodoException exception)
                            {
                                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                                context.Response.ContentType = "application/json";
                                await context.Response.WriteAsync(new ErrorResult
                                {
                                    Code = exception.ExceptionType.ExceptionCode,
                                    Message = exception.ExceptionType.Message
                                }.ToString());
                            }
                        }
                    }
                });
            });
        }
    }
}