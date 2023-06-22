using System.Text.Json.Serialization;

namespace YApiModel.Models;

public class Round
{
    [JsonPropertyName("round_number")]
    public int RoundNumber { get; set; }
    [JsonPropertyName("is_upper")]
    public bool IsUpper { get; set; }
    [JsonPropertyName("games")]
    public List<Game> Games { get; set; } = new();
}
