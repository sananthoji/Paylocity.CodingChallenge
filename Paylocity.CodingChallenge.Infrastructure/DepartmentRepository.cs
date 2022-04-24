using Microsoft.EntityFrameworkCore;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Infrastructure
{
    public class DepartmentRepository : IDepartmentRepository
    {
        IPaylocityCodingChallengeContext paylocityCodingChallengeContext;

        public DepartmentRepository(IPaylocityCodingChallengeContext paylocityCodingChallengeContext)
        {
            this.paylocityCodingChallengeContext = paylocityCodingChallengeContext;
        }

        public async Task<IList<Department>> GetEmployeeDependents()
        {
            var employees = await this.paylocityCodingChallengeContext.Departments.ToListAsync();
            return employees;
        }
    }
}
