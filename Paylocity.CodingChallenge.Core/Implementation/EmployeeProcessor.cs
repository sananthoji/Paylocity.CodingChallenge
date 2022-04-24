using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Core.Implementation
{
    public class EmployeeProcessor : IEmployeeProcessor
    {
        private readonly ILogger logger;

        private readonly IEmployeesService employeesService;

        public EmployeeProcessor(ILogger<EmployeeProcessor> logger, IEmployeesService employeesService)
        {
            this.logger = logger;
            this.employeesService = employeesService;
        }
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            logger.LogInformation("EmployeeProcessor-GetEmployee-Start");
            
            var employee = await employeesService.GetEmployeeAsync(employeeId).ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployee-End");

            return employee;
        }

        public async Task<EmployeeDeductions> GetEmployeeDeductionAsync(int employeeId)
        {
            logger.LogInformation("EmployeeProcessor-GetEmployee-Start");

            var employee = await employeesService.GetEmployeeDeducitons(employeeId).ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployee-End");

            return employee;
        }

        public async Task<IList<Employee>> GetEmployeesAsync()
        {
            logger.LogInformation("EmployeeProcessor-GetEmployees-Start");

            var employees = await employeesService.GetEmployeesAsync().ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployees-End");

            return employees;
        }
    }
}
