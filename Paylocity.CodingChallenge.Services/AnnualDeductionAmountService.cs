using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities.Enum;
using Paylocity.CodingChallenge.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Services
{
    public class AnnualDeductionAmountService : IAnnualDeductionAmountService
    {
        private readonly int employeeDeductionPerYear;

        private readonly int spouseDeductionPerYear;

        private readonly int childDeductionPerYear;

        private readonly int payChecksPerYear;

        private readonly ILogger<AnnualDeductionAmountService> logger;

        public AnnualDeductionAmountService(IConfiguration configuration, ILogger<AnnualDeductionAmountService> logger)
        {
            this.employeeDeductionPerYear = Convert.ToInt32(configuration["EmployeeDeductions:EmployeeDedutionPerYear"]);
            this.spouseDeductionPerYear = Convert.ToInt32(configuration["EmployeeDeductions:SpouseDeducitonPerYear"]);
            this.childDeductionPerYear = Convert.ToInt32(configuration["EmployeeDeductions:ChildDeducitonPerYear"]);
            this.payChecksPerYear = Convert.ToInt32(configuration["EmployeeDeductions:NumberOfPaychecksPerYear"]);
            this.logger = logger;
        }

        public decimal GetDeduciton(PersonType personType)
        {
            this.logger.LogInformation("AnnualDeductionAmountService-GetDeduciton-Start/End");
            switch (personType)
            {
                case PersonType.Employee:
                    return employeeDeductionPerYear;
                case PersonType.Spouse:
                    return spouseDeductionPerYear;
                case PersonType.Child:
                    return childDeductionPerYear;
                default: throw new BusinessException("Invalid person type");
            }
        }

        public int GetPaychecksPerYear()
        {
            this.logger.LogInformation("AnnualDeductionAmountService-GetPaychecksPerYear-Start/End");
            return payChecksPerYear;
        }
    }
}
