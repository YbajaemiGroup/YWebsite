using System.Text.Json.Serialization;

namespace YApiModel.Models;

public class GroupFillData
{
    [JsonPropertyName("group")]
    public int Group { get; set; }
    [JsonPropertyName("player")]
    public int PlayerId { get; set; }
}
