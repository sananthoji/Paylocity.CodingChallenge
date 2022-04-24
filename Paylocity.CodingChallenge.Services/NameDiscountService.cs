using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Services
{
    public class NameDiscountService : INameDiscountService
    {
        private readonly string NameBeginsWith;
        private readonly string DiscountType;
        private readonly decimal Value;

        private readonly ILogger<NameDiscountService> logger;

        public NameDiscountService(IConfiguration configuration, ILogger<NameDiscountService> logger)
        {
            this.NameBeginsWith = configuration["EmployeeDiscounts:NameDiscount:BeginsWith"];
            this.DiscountType = configuration["EmployeeDiscounts:NameDiscount:Type"];
            this.Value = Convert.ToDecimal(configuration["EmployeeDiscounts:NameDiscount:Value"]);
            this.logger = logger;
        }

        public decimal GetNameDiscountService(string name)
        {
            if (name?.ToLower().StartsWith(this.NameBeginsWith) ?? false)
            {
                return this.Value;
            }
            else
                return 0;
        }
    }
}
