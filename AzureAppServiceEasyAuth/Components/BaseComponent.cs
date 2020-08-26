using AzureAppServiceEasyAuth.Models;
using Microsoft.AspNetCore.Components;

namespace AzureAppServiceEasyAuth.Components
{
    public class BaseComponent: ComponentBase
    {
        [CascadingParameter]
        protected LoggedInUser LoggedInUser { get; set; }
    }
}
