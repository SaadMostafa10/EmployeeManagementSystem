using PrimeTech.EMS.BLL.DataTransferObjects.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeToReturnDTO> GetAllEmployees();
        EmployeeDetailsToReturnDTO? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDTO employeeDTO);
        int UpdateEmployee(UpdatedEmployeeDTO employeeDTO);
        bool DeleteEmployee(int id);
    }
}
