using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs
{
    public class DepartmentToReturnDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        [Display(Name ="Date Of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
