using PrimeTech.EMS.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.DataTransferObjects.EmployeeDTOs
{
    public class EmployeeToReturnDTO
    {
        public int Id { get; set; } //PK
        
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        
       
        public string Gender { get; set; } = null!;
        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; } = null!;
    }
}
