using AzureAppServiceEasyAuth.Utilities;
using Microsoft.AspNetCore.Components.Authorization;

namespace AzureAppServiceEasyAuth.Extensions
{
    public static class AuthenticationStateExtension
    {
        public static string ExtractFullName(this AuthenticationState authState)
        {
            return AuthUtil.ExtractFullName(authState);
        }

        public static string ExtractEmailAddress(this AuthenticationState authState)
        {
            return AuthUtil.ExtractEmailAddress(authState);
        }
    }
}
