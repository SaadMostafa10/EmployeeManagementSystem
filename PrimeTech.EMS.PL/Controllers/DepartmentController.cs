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

        // View Storage : ViewData , ViewBag => Deal With The Storage
        // Dictionary
        // Send Extra Data
        // 1. Send Data From Controller [Action] To View
        // 2. Send Data From View To PartialView
        // 3. Send Data From View To Layout




        [HttpGet]  //GET : BaseURL/Department/Index 
        public IActionResult Index()
        {
            // View`s Dictionary : Pass Data From Controller [Action] to View or (From View --> [PartialView , Layout])

            // 1. ViewData : is a Dictionary Type Property (ASP.Net Framework 3.5)
            //             => Property is inherited from Controller Class [Dictionary]
            //             => It`s Helps To Transfer Data From Controller [Action] To View

            // ViewData["Obj"] = "Hello View Data";

            // 1. ViewBag : is a Dynamic Type Property (ASP.Net Framework 4.0)
            //             => Property is inherited from Controller Class [Dictionary]
            //             => It`s Helps To Transfer Data From Controller [Action] To View

            // ViewBag.Obj = "Hello View Bag";
            // ViewBag.Obj = new { name = "saad", Id = 1 };  // RunTime Error

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
        [ValidateAntiForgeryToken]   // Action Filter
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)  // Server Side Validation
                return View(departmentVM);

            var message = string.Empty;
            try
            {
                var CreatedDepartment = new CreatedDepartmentDTO()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    CreationDate = departmentVM.CreationDate,
                    Description = departmentVM.Description,
                };


                var Created = _departmentServices.CreateDepartment(CreatedDepartment) > 0;

                // 1. TempData : is a Dictionary Type Property (ASP.Net Framework 3.5)
                //             => Property is inherited from Controller Class [Dictionary]
                //             => It`s Helps To Transfer Data From Request To Another Request Action To Another Action

                if (Created)
                {
                    TempData["Message"] = "Department is Created Successfuly";
                    return RedirectToAction(nameof(Index));
                }
                    

                else
                {
                    message = "Department is not Created";
                    TempData["Message"] =message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }
                //return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentVM);
                }
                else
                {
                    message = "Department is not Created";
                    return View("Error", message);
                }


            }
        }

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentServices.GetDepartmentById(id.Value);
            return department is null ? NotFound() :View(department);
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

            return View(new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,

            });



        }

        [HttpPost] // POST : BaseURL/Department/Edit/id
        [ValidateAntiForgeryToken]   // Action Filter
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
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
        // [HttpGet] // GET :  BaseURL/Department/Delete/id  // GET => Not Work in Modal You Can Delete it [GET]
        // public IActionResult Delete(int? id)
        // {
        //     if (id == null)
        //         return BadRequest(); //400
        //     var department = _departmentServices.GetDepartmentById(id.Value);
        // 
        //     if (department == null)
        //         return NotFound(); // 404
        // 
        //     return View(department);
        // }
       
        [HttpPost]
        [ValidateAntiForgeryToken]   // Action Filter
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
