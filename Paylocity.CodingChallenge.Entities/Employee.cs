using System;
using System.Collections.Generic;

namespace Paylocity.CodingChallenge.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeDependents = new HashSet<EmployeeDependent>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmployeeId { get; set; }
        public int? DepartmentId { get; set; }

        public int? AnnualSalary { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? JoiningDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<EmployeeDependent> EmployeeDependents { get; set; }
    }
}
