using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.API.IO.Exceptions
{
    public class InvalidDataException : CoreException
    {
        public const int CODE = 5;

        public InvalidDataException(string message) 
            : base(false, 5, message)
        {
        }
    }
}
