using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Threading.Tasks;

namespace ToastMessage.Components
{

    public class BaseComponent : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Yield();

            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("deleted", out var deleted))
            {
                
            }
        }

        protected async Task OnOperationCompleted()
        {

        }
    }
}

