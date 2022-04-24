using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Framework.Model
{
    public class ErrorDetails
    {
        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public Exception? DetailedError { get; set; }

    }
}
