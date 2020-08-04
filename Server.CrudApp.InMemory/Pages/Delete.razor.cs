using Domain.InMemory.Models;
using Domain.InMemory.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Server.CrudApp.InMemory.Pages
{
    public class DeleteBase: ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        protected IEmployeeService EmployeeService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }


        protected Employee Employee = new Employee();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.FindEmployeeAsync(Id);
        }

        protected async Task PerformDeletion()
        {
            var deleted = await EmployeeService.DeleteEmployeeAsync(Id);

            if (deleted)
            {
                NavigationManager.NavigateTo("/");
            }

            Debug.Write("Failed to delete");
        }
    }
}
