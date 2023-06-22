using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YApiModel
{
    public class Request
    {
        [JsonPropertyName("data")]
        public object Data { get; set; }

        public Request(object data)
        {
            Data = data;
        }
    }
}
