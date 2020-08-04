using Domain.InMemory.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Domain.InMemory.Services
{
    // CRUD operations
    public interface IEmployeeService
    {
        // C -> Create
        Task<Guid> CreateEmployeeAsync(Employee newEmployee);


        // R -> Read
        Task<List<Employee>> GetEmployeesAsync(); // all
        Task<Employee> FindEmployeeAsync(Guid employeeId); // single employee
        Task<bool> EmployeeExistsAsync(Guid employeeId); // uses FindEmployeeAsync() in method body


        // U -> Update
        Task UpdateEmployeeAsync(Employee modifiedEmployee); // uses SaveChangesAsync() in method body


        // D - Delete
        Task<bool> DeleteEmployeeAsync(Guid employeeId); // uses FindEmployeeAsync() + SaveChangesAsync() in method body
    }
}
