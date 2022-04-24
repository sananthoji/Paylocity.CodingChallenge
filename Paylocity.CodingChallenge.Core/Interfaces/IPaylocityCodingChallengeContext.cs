using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Paylocity.CodingChallenge.Entities;

namespace Paylocity.CodingChallenge.Core.Interfaces
{
    public interface IPaylocityCodingChallengeContext
    {
        DatabaseFacade Database { get; }
        DbSet<Department> Departments { get; set; }
        DbSet<DependentType> DependentTypes { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeDependent> EmployeeDependents { get; set; }
    }
}
