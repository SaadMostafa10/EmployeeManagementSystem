using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeTech.EMS.DAL.Models.Identity;
using PrimeTech.EMS.PL.Models.Identity;
using System.Threading.Tasks;

namespace PrimeTech.EMS.PL.Controllers
{
    //[AllowAnonymous] // By Default
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

        }
        #endregion

        #region SignIn
        // Get => Return Form
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        // Post => Submit Form
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // 1] Find User By Email (FindByEmailAsync)
            // 2] If User [is not null]
            // 3] Chek Password is True Or Not (CheckPasswordAsync)
            // 4] SignIn (_signInManager.PasswordSignInAsync)
            // 5] Check Acc is Not Allowed or Locked

            // 6] if Acc is Succedded RedirectToAction()
            // 7] Global Error if [Email or Password] Not Correct 

            var User = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (User is { })  // = If User is not null 
            {
                var flag = await _userManager.CheckPasswordAsync(User, loginViewModel.Password);

                if (flag) // User With Email Exist & Password Correct 
                {
                    var Result = await _signInManager.PasswordSignInAsync(User,
                        loginViewModel.Password, loginViewModel.RememberMe,
                        false); // Make SignIn

                    if (Result.IsNotAllowed)
                        ModelState.AddModelError("", "Your Account Is NotAllowed"); // Global Error

                    if (Result.IsLockedOut)
                        ModelState.AddModelError("", "Your Account Is Locked");

                    if (Result.Succeeded) 
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                    

            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt"); // Global Error

            return View(loginViewModel);

        }
        #endregion

        #region SignOut
        [HttpGet]
        public async new Task<IActionResult> SignOut()
        {
            // Delete Token
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion
    }
}

