using System.Text.Json.Serialization;
using YCore.Exceptions;

namespace YCore.API.IO
{
    internal class Response
    {
        [JsonPropertyName("error"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CoreException? Exception { get; set; }
        [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? ResponseData { get; set; }
    }
}
