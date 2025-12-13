using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeTech.EMS.DAL.Models.Identity;
using PrimeTech.EMS.PL.Models.Identity;
using System.Threading.Tasks;

namespace PrimeTech.EMS.PL.Controllers
{
    public class UserController(UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

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
    }
}
