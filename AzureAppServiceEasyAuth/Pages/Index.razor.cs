using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;


namespace AzureAppServiceEasyAuth.Pages
{
	public class IndexBase : ComponentBase
	{
		public const string PREFERRED_USERNAME = "preferred_username";
		public const string DefaultLoggedInUser = "unknown (default)";

		[CascadingParameter]
		private Task<AuthenticationState> AuthenticationStateTask { get; set; }

		public string CurrentLoggedInUser { get; set; } = string.Empty;

		public IEnumerable<Claim> LoggedInUserClaims { get; set; } = Enumerable.Empty<Claim>();

		protected override async Task OnInitializedAsync()
		{
			var authState = await AuthenticationStateTask;

			LoggedInUserClaims = authState.User?.Claims;

			var loggedInUser = authState.User?.Claims?.FirstOrDefault(c => c.Type == PREFERRED_USERNAME)?.Value;

			if (string.IsNullOrWhiteSpace(loggedInUser))
			{
				loggedInUser = DefaultLoggedInUser;
			}

			CurrentLoggedInUser = loggedInUser;
		}
	}
}
