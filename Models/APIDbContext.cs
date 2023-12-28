using System;
using EmployeeWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebAPI
{
    public class APIDbContext: DbContext
    {
        public APIDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Department> Departments
        {
            get;
            set;
        }
        public DbSet<EmployeeData> Employees
        {
            get;
            set;
        }
    }
}

