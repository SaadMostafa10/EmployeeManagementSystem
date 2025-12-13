using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PrimeTech.EMS.BLL.Common.Services.EmailSender;
using PrimeTech.EMS.DAL.Models.Identity;
using PrimeTech.EMS.DAL.Models.Shared;
using PrimeTech.EMS.PL.Models.Identity;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PrimeTech.EMS.PL.Controllers
{
    //[AllowAnonymous] // By Default
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // SignUp(Register) , SignIn(LogIn) , SignOut , Reset Password

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

        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword() => View(); // Return Form With One Input Field [Email]

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            // ForgetPassword => Hamada@gmail.com [User Not Exist]
            if (ModelState.IsValid)
            {
                // 1] Check user is exist
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)  //=> Send Email => To , Subject , Body
                {
                    // BaseUrl/Account/ResetPassword?Email=saad@gmail.com&Token=vaqjfkaswf172bsk0
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new { forgetPasswordViewModel.Email, token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset Your Password",
                        // BaseUrl/Account/ResetPassword?Email=saad@gmail.com
                        Body = url // ==> go to ResetPassword [Form] contain New Password - Confirm New Password
                    };
                    // SendEmail
                    _emailSender.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                    // UserDefinedDataType [Email] => To string , Subject string , Body string

                }
                else
                    ModelState.AddModelError("", "Invalid Operation Please Try Again");
            }
            return View(forgetPasswordViewModel); // -> [Form] not exist Email
        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();

        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
            // Pass email , token
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                // 1] check user [email] is exist
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token,resetPasswordViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(LogIn));
                    }


                }
                
            }
            ModelState.AddModelError("", "Invalid Operation , Please Try Again ");
            return View(resetPasswordViewModel);

            #endregion
        }

    }
}

