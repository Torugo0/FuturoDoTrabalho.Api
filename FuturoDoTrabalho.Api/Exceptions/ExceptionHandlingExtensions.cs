using FuturoDoTrabalho.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FuturoDoTrabalho.Api.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    var exception = exceptionHandlerPathFeature?.Error;

                    int statusCode = StatusCodes.Status500InternalServerError;
                    string title = "Erro interno no servidor";

                    if (exception is DomainException)
                    {
                        statusCode = StatusCodes.Status400BadRequest;
                        title = "Erro de domínio";
                    }

                    var problem = new ProblemDetails
                    {
                        Status = statusCode,
                        Title = title,
                        Detail = exception?.Message
                    };

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/problem+json";

                    await context.Response.WriteAsJsonAsync(problem);
                });
            });
        }
    }
}
