using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Domain.InMemory.Models;

namespace Domain.InMemory.Services
{
    // Register to DI in Startup class as singleton
    public class InMemoryEmployeeService : IEmployeeService
    {

        #region Initialization

        private List<Employee> _employees = new List<Employee>();

        private async void Initialize()
        {
            _employees = await DevelopmentTimeDataProvider.GetEmployeesAsync();
        }

        #endregion

        public InMemoryEmployeeService()
        {
            Initialize();
        }

        public async Task<string> CreateEmployeeAsync(Employee newEmployee)
        {
            return await Task.Run(new Func<string>(() =>
            {

                var newId = Guid.NewGuid().ToString();
                newEmployee.Id = newId;

                _employees.Add(newEmployee);

                return newId;
            }));
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            if(!_employees.Any())
            {
                _employees = await DevelopmentTimeDataProvider.GetEmployeesAsync();
            }

            return _employees;
        }

        public async Task<Employee> FindEmployeeAsync(string employeeId)
        {
            return await Task.Run(new Func<Employee>(() =>
            {
                var employee = _employees.Find(e => e.Id == employeeId);

                return employee;
            }));
        }

        public async Task<bool> EmployeeExistsAsync(string employeeId)
        {
            return (await FindEmployeeAsync(employeeId) != null);
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeId)
        {
            var employee = await FindEmployeeAsync(employeeId);

            if (employee == null)
            {
                return false;
            }

            return await Task.Run(new Func<bool>(() =>
            {
                _employees.Remove(employee);

                return true;
            }));
        }

        public async Task UpdateEmployeeAsync(Employee modifiedEmployee)
        {
            var employee = await FindEmployeeAsync(modifiedEmployee.Id);
            employee.FirstName = modifiedEmployee.FirstName;
            employee.LastName = modifiedEmployee.LastName;
            employee.Email = modifiedEmployee.Email;
            employee.EmployedRegion = modifiedEmployee.EmployedRegion;
            employee.Rank = modifiedEmployee.Rank;
        }
    }


    #region DevelopmentTimeDataProvider

    public class DevelopmentTimeDataProvider
    {

        #region Private Members

        private const string avatarBase64String = @"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";

        private static List<string> Regions = Enum.GetNames(typeof(Region)).ToList();
        private static List<Rank> Ranks { get; set; } = new List<Rank> {
            new Rank(){ Level = 1, Name = "Trainee" },
            new Rank(){ Level = 2, Name = "System Engineer" },
            new Rank(){ Level = 3, Name = "Senior System Engineer" },
            new Rank(){ Level = 4, Name = "Technology Analyst" },
            new Rank(){ Level = 5, Name = "Technology Lead" },
            new Rank(){ Level = 6, Name = "Technology Architect" },
        };

        #endregion


        public static async Task<List<Employee>> GetEmployeesAsync()
        {
            return await Task.Run(new Func<List<Employee>>(() =>
            {

                return new List<Employee>() {

                    new Employee(){ Id = Guid.NewGuid().ToString(),
                        FirstName = "MD TAREQ",
                        LastName = "HASSAN",
                        Email = "hassan@hovermind.com",
                        JoinDate = Convert.ToDateTime("2020/04/27"),
                        EmployedRegion = Region.America,
                        Rank = Ranks.ElementAt(3),
                        AvatarIcon = avatarBase64String
                    },

                    new Employee(){ Id = Guid.NewGuid().ToString(),
                        FirstName = "JIM",
                        LastName = "BORDEN",
                        Email = "jim@hovermind.com",
                        JoinDate = Convert.ToDateTime("2010/05/11"),
                        EmployedRegion = Region.America,
                        Rank = Ranks.ElementAt(4),
                        AvatarIcon = avatarBase64String
                    },

                    new Employee(){ Id = Guid.NewGuid().ToString(),
                        FirstName = "OLEKSANDR",
                        LastName = "DROPAILO",
                        Email = "olek@hovermind.com",
                        JoinDate = Convert.ToDateTime("2012/02/09"),
                        EmployedRegion = Region.Europe,
                        Rank = Ranks.ElementAt(2),
                        AvatarIcon = avatarBase64String
                    },

                    new Employee(){ Id = Guid.NewGuid().ToString(),
                        FirstName = "SHEIKH",
                        LastName = "ASHADUZZAMAN",
                        Email = "asad@hovermind.com",
                        JoinDate = Convert.ToDateTime("2013/09/14"),
                        EmployedRegion = Region.Asia,
                        Rank = Ranks.ElementAt(3),
                        AvatarIcon = avatarBase64String
                    },

                    new Employee(){ Id = Guid.NewGuid().ToString(),
                        FirstName = "SAHIDUL",
                        LastName = "ISLAM",
                        Email = "sahidul@hovermind.com",
                        JoinDate = Convert.ToDateTime("2015/03/25"),
                        EmployedRegion = Region.Africa,
                        Rank = Ranks.ElementAt(2),
                        AvatarIcon = avatarBase64String
                    }
                };
            }));
        }
    }

    #endregion
}