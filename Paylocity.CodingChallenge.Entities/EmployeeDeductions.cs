using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Entities
{
    public class EmployeeDeductions
    {
        public EmployeeDeductions()
        {
			PayChecksPerYear = 26;
        }
		public string Name { get; set; }

		public int DependentCount { get; set; }

		public decimal AnnualSalary { get; set; }

		public decimal MonthlySalary { get { return AnnualSalary / PayChecksPerYear; } }


		public decimal TotalDeductions { get; set; }

		public decimal FinalAnnualSalary { get { return AnnualSalary - TotalDeductions; } }
		public decimal FinalMonthlySalary { get { return FinalAnnualSalary / PayChecksPerYear; } }

		[Range(1,26)]
		public int PayChecksPerYear { get; set; }
	}
}
