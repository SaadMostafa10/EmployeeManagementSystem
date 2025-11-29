using PrimeTech.EMS.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.DataTransferObjects.EmployeeDTOs
{
    public class EmployeeDetailsToReturnDTO
    {
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; } //User Id
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public string Gender { get; set; } = null!;
        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; } = null!;
        public string? Department { get; set; }
        public string? Image { get; set; }
    }
}
