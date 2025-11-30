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
        Task<IEnumerable<EmployeeToReturnDTO>> GetEmployeesAsync(string search);
        Task<EmployeeDetailsToReturnDTO?> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreatedEmployeeDTO employeeDTO);
        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDTO employeeDTO);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
