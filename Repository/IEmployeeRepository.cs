using System;
using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<EmployeeData>> GetEmployees();
        public Task<EmployeeData> GetEmployeeByID(int ID);
        public Task<EmployeeData> InsertEmployee(EmployeeData objEmployee);
        public Task<EmployeeData> UpdateEmployee(EmployeeData objEmployee);
        public bool DeleteEmployee(int ID);
    }
}

