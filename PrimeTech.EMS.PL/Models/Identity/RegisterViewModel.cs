using System.ComponentModel.DataAnnotations;

namespace PrimeTech.EMS.PL.Models.Identity
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password" , ErrorMessage ="ConfirmPassword doesn`t match Password")]
        public string ConfirmPassword { get; set; } = null!;

        public bool IsAgree { get; set; }

    }
}
