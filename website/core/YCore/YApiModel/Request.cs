using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public override string? ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
