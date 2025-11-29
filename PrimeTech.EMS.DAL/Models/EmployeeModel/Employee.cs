using PrimeTech.EMS.DAL.Models.DepartmentModel;
using PrimeTech.EMS.DAL.Models.Shared;
using PrimeTech.EMS.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace PrimeTech.EMS.DAL.Models.EmployeeModel
{
    public class Employee :BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        // FK
        public int? DepartmentId { get; set; }
        // Navigational Property [One]
        public virtual Department? Department { get; set; }
        public string? Image { get; set; }
    }
}
