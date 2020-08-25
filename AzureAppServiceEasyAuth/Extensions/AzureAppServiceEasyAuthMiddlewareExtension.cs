using AzureAppServiceEasyAuth.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace AzureAppServiceEasyAuth.Extensions
{
    public static class AzureAppServiceEasyAuthMiddlewareExtension
    {
        public static IApplicationBuilder UseAppServiceEasyAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AzureAppServiceEasyAuthMiddleware>();
        }
    }
}
