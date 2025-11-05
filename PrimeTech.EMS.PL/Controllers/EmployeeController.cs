using Microsoft.AspNetCore.Mvc;
using PrimeTech.EMS.BLL.DataTransferObjects.EmployeeDTOs;
using PrimeTech.EMS.BLL.Services.EmployeeServices;
using PrimeTech.EMS.DAL.Models.Department;
using PrimeTech.EMS.DAL.Models.Shared.Enums;


namespace PrimeTech.EMS.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(
          IEmployeeService employeeService,
          ILogger<EmployeeController> logger,
          IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }
        #region Index

        [HttpGet]  //GET : BaseURL/Employee/Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDTO createdEmployeeDTO)
        {
            if (!ModelState.IsValid)  // Server Side Validation
                return View(createdEmployeeDTO);

            var message = string.Empty;
            try
            {
                
                

                var result = _employeeService.CreateEmployee(createdEmployeeDTO);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    message = "Employee is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(createdEmployeeDTO);
                }

            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(createdEmployeeDTO);
                }
                else
                {
                    message = "Employee is not Created";
                    return View("Error", message);
                }


            }
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ? NotFound() :View(employee);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) 
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if(employee is null) 
                return NotFound();
            return View(new UpdatedEmployeeDTO()
            {
                Id = employee.Id,
                Name =employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                

                
            });
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id,UpdatedEmployeeDTO employeeDTO)
        {
            if(!id.HasValue || id != employeeDTO.Id )return BadRequest();
            if(!ModelState.IsValid)return View(employeeDTO);
            var message = string.Empty;
            try
            {
                var Result = _employeeService.UpdateEmployee(employeeDTO);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Updated");
                    return View(employeeDTO);
                }
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeDTO);
                }
                else
                {
                    message = "Employee is not Updated";
                    return View("Error", message);
                }
            }
        }
        #endregion

        #region Delete
        [HttpGet] // GET :  BaseURL/Department/Delete/id  // GET => Not Work in Modal You Can Delete it [GET]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest(); //400
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee == null)
                return NotFound(); // 404

            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            var message = string.Empty;
            try
            {
                var deletedEmployee = _employeeService.DeleteEmployee(id);
                if (deletedEmployee)
                    return RedirectToAction(nameof(Index));
                message = "An Error Occured During Deleting This Employee :(";
                return View(deletedEmployee);

            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "An Error Occured During Updating Employee :(";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
