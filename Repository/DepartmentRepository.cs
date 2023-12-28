using System;
using EmployeeWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly APIDbContext aPIDbContext;
        public DepartmentRepository(APIDbContext context)
        {
            aPIDbContext = context;
            //throw new ArgumentNullException(nameof(context));
        }

        public bool DeleteDepartment(int ID)
        {
            bool result = false;
            var department = aPIDbContext.Departments.Find(ID);
            if (department != null)
            {
                aPIDbContext.Entry(department).State = EntityState.Deleted;
                aPIDbContext.SaveChanges();
                result = true;

            }
            else
                result = false;

            return result;
        }

        public async Task<Department> GetDepartmentById(int ID)
        {
            return await aPIDbContext.Departments.FindAsync(ID);

        }


         public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await aPIDbContext.Departments.ToListAsync();
        }

        public async Task<Department> InsertDepartment(Department department)
        {
            aPIDbContext.Departments.Add(department);
            await aPIDbContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department> UpdateDepartement(Department department)
        {
            aPIDbContext.Entry(department).State = EntityState.Modified;
            await aPIDbContext.SaveChangesAsync();
            return department;
        }

        
    }
}

