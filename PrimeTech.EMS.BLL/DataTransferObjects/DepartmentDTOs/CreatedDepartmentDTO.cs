using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs
{
    public class CreatedDepartmentDTO
    {
        public string Name { get; set; } = null!;
        [Required(ErrorMessage="Code is Required Ya A500Ya !")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateOnly CreationDate { get; set; }

    }
}
