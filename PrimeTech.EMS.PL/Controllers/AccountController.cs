using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeTech.EMS.DAL.Models.Identity;
using PrimeTech.EMS.PL.Models.Identity;
using System.Threading.Tasks;

namespace PrimeTech.EMS.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // SignUp(Register) , SignIn(LogIn) , SignOut ...

        #region Sign Up
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await _userManager.FindByNameAsync(registerViewModel.UserName);
            if (User is { }) // This User already Exist
            {
                ModelState.AddModelError(nameof(registerViewModel.UserName),
                    "This User Name is already Exist for Another Account");
                return View(registerViewModel);
            }

            // Create User 
            User = new ApplicationUser()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                IsAgree = registerViewModel.IsAgree,
            };

            var Result = await _userManager.CreateAsync(User, registerViewModel.Password);

            if (Result.Succeeded)
                return RedirectToAction("LogIn");
            
                // if Result NOT Succeeded => show Error
                foreach (var error in Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(registerViewModel); //=> ModelState Not Valid or Result Not Succeeded

            
            #endregion
        }

    }
}

