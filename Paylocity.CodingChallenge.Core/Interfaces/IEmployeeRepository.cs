using Paylocity.CodingChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IList<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeAsync(int employeeId);
    }
}
