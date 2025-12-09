using System.ComponentModel.DataAnnotations;

namespace PrimeTech.EMS.PL.Models.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
