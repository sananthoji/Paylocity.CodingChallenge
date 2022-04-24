

using Paylocity.CodingChallenge.Entities.Enum;

namespace Paylocity.CodingChallenge.Core.Interfaces
{
    public interface IAnnualDeductionAmountService
    {
        /// <summary>
        /// Get annual deduction amount serivce.
        /// </summary>
        /// <param name="personType"></param>
        /// <returns>return the amount </returns>
        decimal GetDeduciton(PersonType personType);

        /// <summary>
        /// Get Paychecks per year.
        /// </summary>
        /// <returns>the count</returns>
        int GetPaychecksPerYear();
    }
}
