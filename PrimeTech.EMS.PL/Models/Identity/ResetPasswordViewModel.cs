using System.ComponentModel.DataAnnotations;

namespace PrimeTech.EMS.PL.Models.Identity
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
