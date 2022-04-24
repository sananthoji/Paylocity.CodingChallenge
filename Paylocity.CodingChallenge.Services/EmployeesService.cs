using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;
using Paylocity.CodingChallenge.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly ILogger logger;

        private readonly IEmployeeRepository employeeRepository;
        private readonly IAnnualDeductionAmountService annualDeductionAmountService;
        private readonly INameDiscountService nameDiscountService;
        private readonly IDependentTypeRepository dependentTypeRepository;
        public EmployeesService(ILogger<EmployeesService> logger, IEmployeeRepository employeeRepository, IAnnualDeductionAmountService annualDeductionAmountService, INameDiscountService nameDiscountService, IDependentTypeRepository dependentTypeRepository)
        {
            this.logger = logger;
            this.employeeRepository = employeeRepository;
            this.annualDeductionAmountService = annualDeductionAmountService;
            this.nameDiscountService = nameDiscountService;
            this.dependentTypeRepository = dependentTypeRepository;
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            logger.LogInformation("EmployeeProcessor-GetEmployee-Start");

            var employee = await employeeRepository.GetEmployeeAsync(employeeId).ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployee-End");

            return employee;
        }

        public async Task<EmployeeDeductions> GetEmployeeDeducitons(int employeeId)
        {
            logger.LogInformation("EmployeeProcessor-GetEmployeeDeducitons-Start");

            var employee = await this.employeeRepository.GetEmployeeAsync(employeeId).ConfigureAwait(false);
            var employeeDeduction = this.annualDeductionAmountService.GetDeduciton(PersonType.Employee);

            var employeeDeductable = new EmployeeDeductions()
            {
                Name = employee.Name,
                AnnualSalary = employee.AnnualSalary.Value,
                TotalDeductions = employeeDeduction,
            };

            var dependentTypes = await this.dependentTypeRepository.GetDependentTypesAsync().ConfigureAwait(false);

            foreach (var dependent in employee.EmployeeDependents)
            {
                var deductable = this.annualDeductionAmountService.GetDeduciton(GetPersonType(dependent.DependentType, dependentTypes));

                deductable = deductable * (1 - this.nameDiscountService.GetNameDiscountService(dependent.DependentName));

                employeeDeductable.TotalDeductions += deductable;
                employeeDeductable.DependentCount++;
            }

            logger.LogInformation("EmployeeProcessor-GetEmployeeDeducitons-End");

            return employeeDeductable;
        }

        private PersonType GetPersonType(int? dependentType, IList<DependentType> dependentTypes)
        {
            var personType = (PersonType)dependentTypes.FirstOrDefault(x => x.Id == dependentType).DependentType1;

            return personType;
        }

        public async Task<IList<Employee>> GetEmployeesAsync()
        {
            logger.LogInformation("EmployeeProcessor-GetEmployees-Start");

            var employees = await employeeRepository.GetEmployeesAsync().ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployees-End");

            return employees;
        }
    }
}
