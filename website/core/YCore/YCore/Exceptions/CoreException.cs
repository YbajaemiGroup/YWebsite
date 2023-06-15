using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YCore.Exceptions
{
    internal abstract class CoreException
    {
        [JsonPropertyName("error-code")]
        public abstract int Code { get; protected set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public CoreException(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
