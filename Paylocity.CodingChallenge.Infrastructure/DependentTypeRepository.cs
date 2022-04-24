using Microsoft.EntityFrameworkCore;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Infrastructure
{
    public class DependentTypeRepository: IDependentTypeRepository
    {
        IPaylocityCodingChallengeContext paylocityCodingChallengeContext;

        public DependentTypeRepository(IPaylocityCodingChallengeContext paylocityCodingChallengeContext)
        {
            this.paylocityCodingChallengeContext = paylocityCodingChallengeContext;
        }

        public async Task<IList<DependentType>> GetDependentTypesAsync()
        {
            var employees = await this.paylocityCodingChallengeContext.DependentTypes.ToListAsync();
            return employees;
        }
    }
}
