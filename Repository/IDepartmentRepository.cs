using System;
using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.Repository
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetDepartments();
        public Task<Department> GetDepartmentById(int ID);
        public Task<Department> InsertDepartment(Department department);
        public Task<Department> UpdateDepartement(Department department);
        public bool DeleteDepartment(int ID);
    }
}

