using Microsoft.AspNetCore.Builder;
using WebAPI.Middlewares;

namespace WebAPI.Extensions;

public static class CustomExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}

