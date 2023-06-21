using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.API.IO.Exceptions
{
    internal class TokenAuthentificationFailedException : CoreException
    {
        public const int CODE = 1;
        public const string MESSAGE = "Token authentification failed.";

        public TokenAuthentificationFailedException()
            : base(true, CODE, MESSAGE)
        {
        }
    }
}
