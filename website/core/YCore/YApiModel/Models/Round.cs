using System.Text.Json.Serialization;

namespace YApiModel.Models;

public class Round
{
    [JsonPropertyName("round_number")]
    public int RoundNumber { get; set; }
    [JsonPropertyName("games")]
    public List<Game> Games { get; set; } = new();
}
