using Microsoft.EntityFrameworkCore;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Infrastructure
{
    public class EmployeeDependentRepository : IEmployeeDependentRepository
    {
        IPaylocityCodingChallengeContext paylocityCodingChallengeContext;

        public EmployeeDependentRepository(IPaylocityCodingChallengeContext paylocityCodingChallengeContext)
        {
            this.paylocityCodingChallengeContext = paylocityCodingChallengeContext;
        }

        public async Task<IList<EmployeeDependent>> GetEmployeeDependents()
        {
            var employees = await this.paylocityCodingChallengeContext.EmployeeDependents.ToListAsync();
            return employees;
        }
    }
}
