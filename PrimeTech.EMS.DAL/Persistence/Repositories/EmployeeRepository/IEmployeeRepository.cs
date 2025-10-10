using PrimeTech.EMS.DAL.Models.EmployeeModel;
using PrimeTech.EMS.DAL.Persistence.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistence.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {

    }
}
