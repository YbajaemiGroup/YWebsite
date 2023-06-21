using System.Text.Json.Serialization;
using YCore.API.IO.Exceptions;

namespace YCore.API.IO
{
    public class Response
    {
        [JsonPropertyName("error"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CoreException? Exception { get; set; }
        [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? ResponseData { get; set; }
    }
}
