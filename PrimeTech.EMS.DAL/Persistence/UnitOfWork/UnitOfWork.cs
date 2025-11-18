using PrimeTech.EMS.DAL.Persistance.Data.Contexts;
using PrimeTech.EMS.DAL.Persistence.Repositories.DepartmentRepository;
using PrimeTech.EMS.DAL.Persistence.Repositories.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IDepartmentRepository departmentRepository => new DepartmentRepository(_dbContext);
        public IEmployeeRepository employeeRepository => new EmployeeRepository(_dbContext);
        public UnitOfWork(ApplicationDbContext dbContext) // Ask CLR for Creating Object From "ApplicationDbContext"
        {
            _dbContext = dbContext;
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
