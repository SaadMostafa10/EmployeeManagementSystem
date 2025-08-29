using Microsoft.AspNetCore.Mvc;
using PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs;
using PrimeTech.EMS.BLL.Services.DepartmentServices;
using PrimeTech.EMS.PL.Models.Department;

namespace PrimeTech.EMS.PL.Controllers
{
    // DepartmentController Has 2 Relationships
    // 1.Inheritance : DepartmentController Is a Controller
    // 2.Assosiation[Composition] : DepartmentController Has a IDepartmentServices
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
            IDepartmentServices departmentServices,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)

        {
            _departmentServices = departmentServices;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]  //GET : BaseURL/Department/Index 
        public IActionResult Index()
        {
            var departments = _departmentServices.GetAllDepartments();

            return View(departments);
        }
        #endregion

        #region Create
        [HttpGet] //GET : BaseURL/Department/Create
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost] //POST : BaseURL/Department/Create
        public IActionResult Create(CreatedDepartmentDTO department)
        {
            if (!ModelState.IsValid)  // Server Side Validation
                return View(department);

            var message = string.Empty;
            try
            {

                var result = _departmentServices.CreateDepartment(department);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
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
                    return View(department);
                }
                else
                {
                    message = "Department is not Created";
                    return View("Error", message);
                }


            }
        }

        #endregion

        #region Update
        [HttpGet] // GET : BaseURL/Department/Edit/id
        public IActionResult Edit(int? id)
        {

            if (id == null)
                return BadRequest(); // 400

            var department = _departmentServices.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); // 404

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,

            });



        }

        [HttpPost] // POST : BaseURL/Department/Edit/id
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;
            try
            {
                var departmentToUpdate = new UpdatedDepartmentDTO()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };

                var UpdatedDepartment = _departmentServices.UpdateDepartment(departmentToUpdate) > 0;

                if (UpdatedDepartment)
                    return RedirectToAction(nameof(Index));

                message = "An Error Occured During Updating Department :(";
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "An Error Occured During Updating Department :(";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }
        #endregion

        #region Delete
        [HttpGet] // GET :  BaseURL/Department/Delete/id  // GET => Not Work in Modal You Can Delete it [GET]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest(); //400
            var department = _departmentServices.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); // 404

            return View(department);
        }
        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            var message = string.Empty;
            try
            {
                var deletedDepartment = _departmentServices.DeleteDepartment(id);
                if (deletedDepartment)
                    return RedirectToAction(nameof(Index));
                message = "An Error Occured During Deleting This Department :(";
                return View(deletedDepartment);

            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "An Error Occured During Updating Department :(";
            }
            return RedirectToAction(nameof(Index));

        } 
        #endregion

    }
}
