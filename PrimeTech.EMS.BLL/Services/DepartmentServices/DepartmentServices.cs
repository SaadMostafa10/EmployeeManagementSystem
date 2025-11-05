using PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs;
using PrimeTech.EMS.DAL.Models.Department;
using PrimeTech.EMS.DAL.Persistence.Repositories.DepartmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentServices(IDepartmentRepository departmentRepository) // Ask CLR For Creating Object From DepartmentRepository
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmentToReturnDTO> GetAllDepartments()
        {
            // Convert Department To=> DepartmentToReturnDTO
            var departments = _departmentRepository
                .GetIQueryable()
                .Where(D => !D.IsDeleted)
                .Select(department => new DepartmentToReturnDTO
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate,
            });
            return departments; 
        }
        public DepartmentDetailsToReturnDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is { })
            {
                return new DepartmentDetailsToReturnDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                    CreationDate = department.CreationDate

                };
            }
            return null;
        }
        public int CreateDepartment(CreatedDepartmentDTO departmentDTO)
        {
            // CreatedDepartmentDTO => Department [Post] => Submit
            var department = new Department()
            {
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                CreationDate = departmentDTO.CreationDate,
                Description = departmentDTO.Description,
                CreatedBy = 1,
                LastModifiedBy= 1,
                LastModifiedOn=DateTime.UtcNow,


            };

            return _departmentRepository.Add(department); 
        }
        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        {
            // CreatedDepartmentDTO => Department [Post] => Submit
            var department = new Department()
            {
                Id = departmentDTO.Id,
                Name = departmentDTO.Name,
                Code = departmentDTO.Code,
                CreationDate = departmentDTO.CreationDate,
                Description = departmentDTO.Description,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            return _departmentRepository.Update(department);
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department != null)
                return _departmentRepository.Delete(department) > 0;
            return false;
        }

        

        
    }
}
