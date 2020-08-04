using Domain.InMemory.Models;
using Domain.InMemory.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.CrudApp.InMemory.Pages
{
    public class IndexBase: ComponentBase
    {

        [Inject]
        protected IEmployeeService EmployeeService { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();


        protected override async Task OnInitializedAsync()
        {

            Employees = await EmployeeService.GetEmployeesAsync();
        }
    }
}
