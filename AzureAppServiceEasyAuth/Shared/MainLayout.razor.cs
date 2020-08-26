using AzureAppServiceEasyAuth.Extensions;
using AzureAppServiceEasyAuth.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace AzureAppServiceEasyAuth.Shared
{
    public class MainLayoutBase: LayoutComponentBase
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public LoggedInUser CurrentLoggedInUser { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var authState = await AuthenticationStateTask;

            var fullName = authState.ExtractFullName();

            var emailAddress = authState.ExtractEmailAddress();

            CurrentLoggedInUser = new LoggedInUser() { FullName = fullName, EmailAddress = emailAddress };
        }
    }
}
