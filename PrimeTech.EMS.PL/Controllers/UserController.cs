using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PrimeTech.EMS.DAL.Models.Identity;
using PrimeTech.EMS.PL.Models.Identity;
using System.Threading.Tasks;

namespace PrimeTech.EMS.PL.Controllers
{
    public class UserController(
        UserManager<ApplicationUser> userManager ,
        IWebHostEnvironment environment )
        : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IWebHostEnvironment _environment = environment;

        // Service -> Services [UserManager]
        #region Index
        //[HttpGet] by Default
        public async Task<IActionResult> Index(string searchValue)
        {
            // Service => GetAllUsers [UserManager<ApplicationUser>]
            var usersQuery = _userManager.Users.AsQueryable();
            if(!string.IsNullOrEmpty(searchValue))
               usersQuery = usersQuery.Where(u=>u.Email.ToLower().Contains(searchValue.ToLower()));

            var users = usersQuery.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName  = u.LastName,
                Email = u.Email
                // Roles

            }).ToList(); // ->Immediate Execute Now in app [not in DB]

            foreach(var user in users)
                // Handle Roles For Each User
                user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
            return View(users);
            
            
            
        }
        #endregion

        #region Details
        
        public async Task<IActionResult> Details(string? id)
        {
            if(id == null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if(user == null) return NotFound();
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userVM);
        }

        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest(); // 400
            var user = await _userManager.FindByIdAsync(id);
            if(user == null) return NotFound(); // 404
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel, string? id)
        {
            if (!ModelState.IsValid) return View(userViewModel);
            if (userViewModel.Id != id) return BadRequest();

            string message = "";
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "User Can't be Updated";
                }

            }
            catch (Exception ex)
            {
                //if (_environment.IsDevelopment())
                //    message = ex.Message;
                //else
                //    message = "User Can't be Updated";

                message = _environment.IsDevelopment() ? ex.Message : "User Can't be Updated Because Problem Happen";

            }
            ModelState.AddModelError("", message);
            return View(userViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null) return NotFound();

            string message = "";
            try
            {
                var result = await _userManager.DeleteAsync(user);
                if(result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "The User Can't Be Deleted";
                }
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "An Error Happen When Delete The User";
            }
            ModelState.AddModelError("", message);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
