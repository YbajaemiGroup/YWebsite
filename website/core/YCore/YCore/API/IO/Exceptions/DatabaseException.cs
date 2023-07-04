using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.API.IO.Exceptions
{
    public class DatabaseException : CoreException
    {
        public const int CODE = 5;

        public DatabaseException(string code, PostgresException exception) : base(false, CODE, $"Postgres code {code}. {exception.MessageText}", exception)
        {
        }
    }
}
