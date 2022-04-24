using System;
using System.Collections.Generic;

namespace Paylocity.CodingChallenge.Entities
{
    public partial class DependentType
    {
        public DependentType()
        {
            EmployeeDependents = new HashSet<EmployeeDependent>();
        }

        public int Id { get; set; }
        public int? DependentType1 { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<EmployeeDependent> EmployeeDependents { get; set; }
    }
}
