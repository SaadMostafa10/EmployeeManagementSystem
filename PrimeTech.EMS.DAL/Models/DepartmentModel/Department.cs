using PrimeTech.EMS.DAL.Models.EmployeeModel;
using PrimeTech.EMS.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Models.DepartmentModel
{
    public class Department:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
        // Navigational Property [Many] => Will not be Loaded [Related Data]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
