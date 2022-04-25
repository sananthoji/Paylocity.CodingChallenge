using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Paylocity.CodingChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger logger;

        private readonly IEmployeeProcessor employeeProcessor;

        //private readonly IEmployeeDependentRepository;
        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeProcessor employeeProcessor)
        {
            this.logger = logger;
            this.employeeProcessor = employeeProcessor;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<IList<Employee>> GetEmployees()
        {
            logger.LogInformation("EmployeeProcessor-GetEmployee-Start");

            var employees = await employeeProcessor.GetEmployeesAsync().ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployee-End");

            return employees;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<Employee> GetEmployee(int employeeId)
        {
            logger.LogInformation("EmployeeProcessor-GetEmployee-Start");

            var employee = await employeeProcessor.GetEmployeeAsync(employeeId).ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployee-End");

            return employee;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("GetEmployeeDeduction")]
        public async Task<EmployeeDeductions> GetEmployeeDeduction(int employeeId)
        {
            logger.LogInformation("EmployeeProcessor-GetEmployee-Start");

            var employee = await employeeProcessor.GetEmployeeDeductionAsync(employeeId).ConfigureAwait(false);

            logger.LogInformation("EmployeeProcessor-GetEmployee-End");

            return employee;
        }
    }
}
