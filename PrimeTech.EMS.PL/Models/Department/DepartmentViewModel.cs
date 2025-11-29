using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrimeTech.EMS.PL.Models.Department
{
    public class DepartmentViewModel
    {
        public string Name { get; set; } = null!;
        [Required(ErrorMessage ="Code is Required Ya 500YA )!")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [DisplayName("Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
