using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Security.Claims;

namespace AzureAppServiceEasyAuth.Utilities
{
    public class AuthUtil
    {
        public static string ExtractFullName(AuthenticationState authState)
        {
            var surnameClaim = authState.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Surname));
            var givenNameClaim = authState.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.GivenName));

            var surname = string.Empty;
            if (!string.IsNullOrWhiteSpace(surnameClaim?.Value))
            {
                surname = surnameClaim.Value;
            }

            var givenName = string.Empty;
            if (!string.IsNullOrWhiteSpace(givenNameClaim?.Value))
            {
                givenName = givenNameClaim.Value;
            }

            var fullName = string.Empty;
            if (!string.IsNullOrWhiteSpace(surname) && !string.IsNullOrWhiteSpace(givenName))
            {
                fullName = $"{surname} {givenName}";
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {

                fullName = "unknown(default)";

                //
                // Now try to get preferred_username (Azure AD Global provides preferred_username)
                //
                var preferredNameClaim = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("preferred_username"));
                if (!string.IsNullOrWhiteSpace(preferredNameClaim?.Value))
                {
                    fullName = preferredNameClaim.Value;
                }
            }

            return fullName;
        }

        public static string ExtractEmailAddress(AuthenticationState authState)
        {
            var emailClaim = authState.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));

            var emailAddress = string.Empty;
            if (!string.IsNullOrWhiteSpace(emailClaim?.Value))
            {
                emailAddress = emailClaim.Value;
            }

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                emailAddress = "xyz@abc.com(default)";

                //
                // Now try to get upn (upn might be email address)
                //
                var upnClaim = authState.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Upn));
                if (!string.IsNullOrWhiteSpace(upnClaim?.Value))
                {
                    emailAddress = upnClaim.Value;
                }
            }

            return emailAddress;
        }
    }
}
