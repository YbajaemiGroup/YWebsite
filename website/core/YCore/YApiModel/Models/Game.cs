using System.Text.Json.Serialization;

namespace YApiModel.Models;

public class Game
{
    [JsonPropertyName("row")]
    public int Row { get; set; }
    [JsonPropertyName("is_upper")]
    public bool IsUpper { get; set; }
    [JsonPropertyName("player1")]
    public int? Player1Id { get; set; }
    [JsonPropertyName("player2")]
    public int? Player2Id { get; set; }
    [JsonPropertyName("winner")]
    public int? WinnerId { get; set; }
}
