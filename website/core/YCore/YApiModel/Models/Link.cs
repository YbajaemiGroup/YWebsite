using System.Text.Json.Serialization;

namespace YApiModel.Models;

public class Link
{
    [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    [JsonPropertyName("player"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PlayerId { get; set; }
    [JsonPropertyName("url")]
    public string LinkUrl { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }

    public Link(string linkUrl, string description, int? id = null, int? playerId = null)
    {
        Id = id;
        PlayerId = playerId;
        LinkUrl = linkUrl;
        Description = description;
    }
}
