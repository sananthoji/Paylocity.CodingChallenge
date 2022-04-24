using Paylocity.CodingChallenge.Entities.Enum;
using System;
using System.Collections.Generic;

namespace Paylocity.CodingChallenge.Entities
{
    public partial class EmployeeDependent
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? DependentType { get; set; }
        public string? DependentName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual DependentType? DependentTypeNavigation { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
