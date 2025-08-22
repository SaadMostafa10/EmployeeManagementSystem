using Microsoft.EntityFrameworkCore;
using PrimeTech.EMS.DAL.Models.Department;
using PrimeTech.EMS.DAL.Persistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistence.Repositories.DepartmentRepository
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();

            return _dbContext.Departments.ToList();
        }
        // Question not Understand
        public IQueryable<Department> GetAllAsQueryable()
        {
            return _dbContext.Departments;
        }
        public Department? Get(int id)
        {
            return _dbContext.Departments.Find(id);
            // var department = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
            // if (department == null)
            //     department = _dbContext.Departments.FirstOrDefault(D => D.Id == id);
            // 
            // return department;
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }

        
    }
}
