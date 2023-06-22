using System.Text.Json.Serialization;

namespace YApiModel;

public class Response
{
    [JsonPropertyName("error"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public YException? Exception { get; set; }
    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? ResponseData { get; set; }
}
