using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using PrimeTech.EMS.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.DataTransferObjects.EmployeeDTOs
{
    public class CreatedEmployeeDTO
    {
        public string Name { get; set; } = null!;
        [Range(22,30)]
        public int Age { get; set; }
        [DataType(DataType.Currency)]
        [RegularExpression(@"^(\d{1,6})\s+([a-zA-Z0-9\s,.'#-]+)$",
        ErrorMessage = "The address must start with a building number followed by the street name (e.g., '16 Tahrir St').")]
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; } 
        public EmployeeType EmployeeType { get; set; }
        [Display(Name="Department")]
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }
        //public string? ImageUrl { get; set; }
    }
}
