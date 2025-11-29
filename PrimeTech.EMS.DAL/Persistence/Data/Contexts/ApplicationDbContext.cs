using Microsoft.EntityFrameworkCore;
using PrimeTech.EMS.DAL.Models.DepartmentModel;
using PrimeTech.EMS.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistance.Data.Contexts
{
    // Repo => Communicate with AppDbContext
    // Department Repo => Create Object From AppDbContext To Open Connection With DB
    // Employee   Repo => Create Object From AppDbContext To Open another Connection With DB

    public class ApplicationDbContext:DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Apply All Configurations Classes // Fluent API
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
