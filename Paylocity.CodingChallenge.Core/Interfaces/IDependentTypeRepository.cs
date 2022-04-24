using Paylocity.CodingChallenge.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Core.Interfaces
{
    public interface IDependentTypeRepository
    {
        Task<IList<DependentType>> GetDependentTypesAsync();
    }
}
