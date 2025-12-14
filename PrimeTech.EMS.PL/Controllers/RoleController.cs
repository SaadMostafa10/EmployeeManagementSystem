using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeTech.EMS.DAL.Models.Identity;
using PrimeTech.EMS.PL.Models.Identity;
using PrimeTech.EMS.PL.Models.Role;
using System.Threading.Tasks;

namespace PrimeTech.EMS.PL.Controllers
{
    public class RoleController(
       RoleManager<IdentityRole> roleManager,
       IWebHostEnvironment environment)
       : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IWebHostEnvironment _environment = environment;

        // Service -> Services [UserManager]
        #region Index
        //[HttpGet] by Default
        public async Task<IActionResult> Index(string searchValue)
        {
            // Service => GetAllUsers [UserManager<ApplicationUser>]
            var roleQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
                roleQuery = roleQuery.Where(r => r.Name.ToLower().Contains(searchValue.ToLower()));

            var roles = await roleQuery.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                

            }).ToListAsync(); // ->Immediate Execute Now in app [not in DB]

            return View(roles);



        }
        #endregion

        #region Details

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleVM);
        }

        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest(); // 400
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound(); // 404
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel roleViewModel, string? id)
        {
            if (!ModelState.IsValid) return View(roleViewModel);
            if (roleViewModel.Id != id) return BadRequest();

            string message = "";
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound();
                role.Id = roleViewModel.Id;
                role.Name = roleViewModel.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Role Can't be Updated";
                }

            }
            catch (Exception ex)
            {
                //if (_environment.IsDevelopment())
                //    message = ex.Message;
                //else
                //    message = "User Can't be Updated";

                message = _environment.IsDevelopment() ? ex.Message : "Role Can't be Updated Because Problem Happen";

            }
            ModelState.AddModelError("", message);
            return View(roleViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            string message = "";
            try
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "The Role Can't Be Deleted";
                }
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "An Error Happen When Delete The Role";
            }
            ModelState.AddModelError("", message);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if(ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = roleViewModel.Name,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }
        #endregion
    }
}
