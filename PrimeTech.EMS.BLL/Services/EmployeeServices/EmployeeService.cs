using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrimeTech.EMS.BLL.Common.Services.AttachmentService;
using PrimeTech.EMS.BLL.DataTransferObjects.EmployeeDTOs;
using PrimeTech.EMS.DAL.Models.EmployeeModel;
using PrimeTech.EMS.DAL.Persistence.Repositories.EmployeeRepository;
using PrimeTech.EMS.DAL.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork 
                              ,IAttachmentService attachmentService) 
        // Ask CLR for Creating Object From EmployeeRepository
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }

        public async Task<IEnumerable<EmployeeToReturnDTO>> GetEmployeesAsync(string search)
        {
            var employees = _unitOfWork.employeeRepository
            .GetIQueryable()
            .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
            .Include(E=> E.Department)
            .Select(employee => new EmployeeToReturnDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive= employee.IsActive,
                Salary = employee.Salary,
                Email= employee.Email,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                Department = employee.Department.Name,
                Image = employee.Image, // To Show Image In Index View
            }).ToListAsync();
            return await employees;
                                             
        }

        public async Task<EmployeeDetailsToReturnDTO?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(id);
            if (employee != null)
                return new EmployeeDetailsToReturnDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Address = employee.Address,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    Department = employee.Department?.Name ?? "" ,// Once Access Department [Name]
                    Image = employee.Image,  // To Show in Details  string?
                };
            return null;
        }

        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDTO employeeDTO)
        {
            var employee = new Employee()
            {
                Name = employeeDTO.Name,
                Age = employeeDTO.Age,
                Address = employeeDTO.Address,
                Salary = employeeDTO.Salary,
                IsActive = employeeDTO.IsActive,
                Email = employeeDTO.Email,
                PhoneNumber = employeeDTO.PhoneNumber,
                HiringDate = employeeDTO.HiringDate,
                Gender = employeeDTO.Gender,
                EmployeeType = employeeDTO.EmployeeType,
                DepartmentId = employeeDTO.DepartmentId,
                CreatedBy = 1,  // UserId
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                //Image = employeeDTO.Image, 

            };

            if (employeeDTO.Image is not null)
                employee.Image = await _attachmentService.UploadAsync(employeeDTO.Image, "imgs");

            _unitOfWork.employeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDTO employeeDTO)
        {
            var employee = new Employee()
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                Age = employeeDTO.Age,
                IsActive = employeeDTO.IsActive,
                Salary = employeeDTO.Salary,
                Address = employeeDTO.Address,
                PhoneNumber = employeeDTO.PhoneNumber,
                Email = employeeDTO.Email,
                HiringDate = employeeDTO.HiringDate,
                Gender = employeeDTO.Gender,
                EmployeeType = employeeDTO.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedOn = DateTime.UtcNow,
                DepartmentId = employeeDTO.DepartmentId

            };
            _unitOfWork.employeeRepository.Update(employee);
            return await _unitOfWork.CompleteAsync();

        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.employeeRepository;
            var employee = await employeeRepo.GetAsync(id);
            if (employee != null)
                 employeeRepo.Delete(employee);
            return await _unitOfWork.CompleteAsync() > 0;

        }

        
    }
}
