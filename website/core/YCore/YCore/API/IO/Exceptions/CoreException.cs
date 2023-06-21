﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YCore.API.IO.Exceptions
{
    public abstract class CoreException
    {
        [JsonIgnore]
        public bool Expected { get; set; }
        [JsonIgnore]
        public int ErrorCode { get; set; }
        [JsonIgnore]
        public Exception? Exception { get; set; }

        [JsonPropertyName("error-code")]
        public string Code { get => $"{(Expected ? "0" : "1")}x{ErrorCode}"; }
        [JsonPropertyName("message")]
        public string Message { get; set; }

        protected CoreException(bool expected, int errorCode, string message, Exception? exception = null)
        {
            Expected = expected;
            ErrorCode = errorCode;
            Exception = exception;
            Message = message;
        }
    }
}
