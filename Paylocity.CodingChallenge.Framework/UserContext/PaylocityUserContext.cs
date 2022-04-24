﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Framework
{
    internal class PaylocityUserContext
    {
        public int Id { get; set; }

        public Guid? UserId { get; set; }

        public string Email { get; set; }

        public string? Name { get; set; }

        public string? EmployeeId { get; set; }

        public int? DepartmentId { get; set; }

    }
}
