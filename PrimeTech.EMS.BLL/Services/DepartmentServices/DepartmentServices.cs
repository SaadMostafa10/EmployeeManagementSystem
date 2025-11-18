using AutoMapper;
using PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs;
using PrimeTech.EMS.DAL.Models.DepartmentModel;
using PrimeTech.EMS.DAL.Persistence.Repositories.DepartmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PrimeTech.EMS.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentServices(IDepartmentRepository departmentRepository ,IMapper mapper) // Ask CLR For Creating Object From DepartmentRepository
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public IEnumerable<DepartmentToReturnDTO> GetAllDepartments()
        {

            // Convert Department To=> DepartmentToReturnDTO
            var departments = _departmentRepository
                .GetIQueryable()
                .Where(D => !D.IsDeleted);
                //.Select(department => new DepartmentToReturnDTO
                //{
                //    Id = department.Id,
                //    Name = department.Name,
                //    Code = department.Code,
                //    CreationDate = department.CreationDate,
                //});
                //return departments;
            
            // Best practice مع AutoMapper
            return _mapper.ProjectTo<DepartmentToReturnDTO>(departments).ToList();
             
        }
        public DepartmentDetailsToReturnDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department is { })
            {
                var departmentDetails = _mapper.Map<Department, DepartmentDetailsToReturnDTO>(department);
                return departmentDetails;

                // Manual Mapping
                ///return new DepartmentDetailsToReturnDTO
                ///{
                ///    Id = department.Id,
                ///    Name = department.Name,
                ///    Code = department.Code,
                ///    Description = department.Description,
                ///    CreatedBy = department.CreatedBy,
                ///    CreatedOn = department.CreatedOn,
                ///    LastModifiedBy = department.LastModifiedBy,
                ///    LastModifiedOn = department.LastModifiedOn,
                ///    CreationDate = department.CreationDate
                ///
                ///};
            }
            return null;
        }
        public int CreateDepartment(CreatedDepartmentDTO departmentDTO)
        {
            // CreatedDepartmentDTO => Department [Post] => Submit

            var department = _mapper.Map<CreatedDepartmentDTO, Department>(departmentDTO);
            department.CreatedBy = 1;
            department.LastModifiedBy = 1;
            department.LastModifiedOn= DateTime.UtcNow;

            // Manual Mapping
            ///var department = new Department()
            ///{
            ///    Code = departmentDTO.Code,
            ///    Name = departmentDTO.Name,
            ///    CreationDate = departmentDTO.CreationDate,
            ///    Description = departmentDTO.Description,
            ///    CreatedBy = 1,
            ///    LastModifiedBy= 1,
            ///    LastModifiedOn=DateTime.UtcNow,
            ///
            ///
            ///};

            return _departmentRepository.Add(department); 
        }
        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        {
            // جلب الـ entity من DB
            // var department = _departmentRepository.Get(departmentDTO.Id);
            // 
            // // لو مش موجود، رجع 0 أو أي قيمة تشير للفشل
            // if (department == null)
            //     return 0;
            // UpdatedDepartmentDTO => Department [Post] => Submit

            var department = _mapper.Map<UpdatedDepartmentDTO, Department>(departmentDTO);
            department.LastModifiedBy = 1;
            department.LastModifiedOn = DateTime.UtcNow;
            
            ///var department = new Department()
            ///{
            ///    Id = departmentDTO.Id,
            ///    Name = departmentDTO.Name,
            ///    Code = departmentDTO.Code,
            ///    CreationDate = departmentDTO.CreationDate,
            ///    Description = departmentDTO.Description,
            ///    LastModifiedBy = 1,
            ///    LastModifiedOn = DateTime.UtcNow,
            ///};
           
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
