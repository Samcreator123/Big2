using Big2.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Big2.Controller.Extension;

public static class ExceptionHandler
{
    /// <summary>
    /// 統一處理錯誤，撰寫錯誤回覆、記錄錯誤
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseBig2ExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (exception == null)
                    return;

                await WriteErrorResponse(context, exception);
            });
        });

        return builder;
    }

    // 需改成符合 RFC 7807 標準的回應格式
    private static async Task WriteErrorResponse(HttpContext context, Exception? exception)
    {
        (int statusCode, string errorSummary, string errMessage) = MappingException(exception);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            errorSummary,
            errMessage,
            apiName = context.Request.Path.Value?.Split('/').LastOrDefault() ?? "Unknown Api",
        });
    }

    private static (int statusCode, string errorSummary, string errMessage) MappingException(Exception? exception)
    {
        return exception switch
        {
            InvalidPlayerCountException ex => (StatusCodes.Status400BadRequest, "Invalid Player Count", ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred.")
        };
    }
}
