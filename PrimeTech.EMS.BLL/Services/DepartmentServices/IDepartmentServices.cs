using PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsToReturnDTO?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDTO departmentDTO);
        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
