using System.Text.Json.Serialization;

namespace YApiModel;

public class YException : Exception
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("message")]
    public override string Message { get; }

    [JsonIgnore]
    public bool Expected { get => Code.Split('x')[0] == "0"; }
    [JsonIgnore]
    public int ErrorCode { get => Convert.ToInt32(Code.Split('x')[1]); }

    public YException(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public override string ToString() => $"{base.ToString()}\nCode: {Code}\n{Message}";
}
