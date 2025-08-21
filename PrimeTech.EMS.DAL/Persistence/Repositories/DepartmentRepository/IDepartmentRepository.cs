using PrimeTech.EMS.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistence.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool WithAsNoTracking = true);
        Department? Get(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);

    }
}
