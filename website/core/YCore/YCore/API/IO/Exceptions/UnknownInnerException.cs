using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.API.IO.Exceptions
{
    internal class UnknownInnerException : CoreException
    {
        public const int CODE = 4;
        public const string MESSAGE = "Unknown inner error raised. ";

        public UnknownInnerException(Exception? exception = null, string? message = null) : 
            base(false, CODE, MESSAGE + message ?? string.Empty, exception)
        {
        }
    }
}
