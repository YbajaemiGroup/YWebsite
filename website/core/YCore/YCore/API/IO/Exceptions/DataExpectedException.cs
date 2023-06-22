using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.API.IO.Exceptions
{
    internal class DataExpectedException : CoreException
    {
        public const int CODE = 2;
        public const string MESSAGE = "Request data (json) expected.";

        public DataExpectedException()
            : base(false, CODE, MESSAGE)
        {
        }
    }
}
