using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs
{
    public class DepartmentDetailsToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public int CreatedBy { get; set; } 
        public string? Description { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
