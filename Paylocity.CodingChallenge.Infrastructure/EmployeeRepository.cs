using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IPaylocityCodingChallengeContext paylocityCodingChallengeContext;

        private readonly ILogger logger;

        public EmployeeRepository(IPaylocityCodingChallengeContext paylocityCodingChallengeContext, ILogger<EmployeeRepository> logger)
        {
            this.paylocityCodingChallengeContext = paylocityCodingChallengeContext;
            this.logger = logger;
        }

        public async Task<IList<Employee>> GetEmployeesAsync()
        {
            logger.LogInformation("EmployeeRepository-GetEmployee-Start");

            var employees = await this.paylocityCodingChallengeContext.Employees.ToListAsync().ConfigureAwait(false);

            logger.LogInformation("EmployeeRepository-GetEmployee-Start");

            return employees;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            logger.LogInformation("EmployeeRepository-GetEmployee-Start");

            var employee = await this.paylocityCodingChallengeContext.Employees.Include(x=>x.EmployeeDependents).FirstOrDefaultAsync(x => x.Id == employeeId).ConfigureAwait(false);

            logger.LogInformation("EmployeeRepository-GetEmployee-Start");

            return employee;
        }


    }
}
