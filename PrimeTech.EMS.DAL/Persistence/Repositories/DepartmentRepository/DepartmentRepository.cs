using Microsoft.EntityFrameworkCore;
using PrimeTech.EMS.DAL.Models.DepartmentModel;
using PrimeTech.EMS.DAL.Persistance.Data.Contexts;
using PrimeTech.EMS.DAL.Persistence.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistence.Repositories.DepartmentRepository
{
    public class DepartmentRepository:GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext)
        // Ask CLR For Creating Object From ApplicationDbContext   
        {

        }

       
    }
}
