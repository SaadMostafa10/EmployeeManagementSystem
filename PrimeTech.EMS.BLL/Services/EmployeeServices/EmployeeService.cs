using Microsoft.EntityFrameworkCore;
using PrimeTech.EMS.BLL.DataTransferObjects.EmployeeDTOs;
using PrimeTech.EMS.DAL.Models.EmployeeModel;
using PrimeTech.EMS.DAL.Persistence.Repositories.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) 
        // Ask CLR for Creating Object From EmployeeRepository
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeToReturnDTO> GetAllEmployees()
        {
            var employees = _employeeRepository
            .GetIQueryable()
            .Where(E => !E.IsDeleted)
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
            }).ToList();
            return employees;
                                             
        }

        public EmployeeDetailsToReturnDTO? GetEmployeeById(int id)
        {
            var employee = _employeeRepository .Get(id);
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
                    Department = employee.Department?.Name ?? "" // Once Access Department [Name]
                };
            return null;
        }

        public int CreateEmployee(CreatedEmployeeDTO employeeDTO)
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

            };
            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDTO employeeDTO)
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
            return _employeeRepository.Update(employee);

        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee != null)
                return _employeeRepository.Delete(employee) > 0;
            return false;

        }
    }
}
