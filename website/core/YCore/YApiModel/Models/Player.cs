using System.Text.Json.Serialization;

namespace YApiModel.Models;

public class Player
{
    [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    [JsonPropertyName("nickname")]
    public string NickName { get; set; }
    [JsonPropertyName("image_name")]
    public string? ImageName { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("group_number")]
    public int? GroupNumber { get; set; }
    [JsonPropertyName("won")]
    public int? Won { get; set; }
    [JsonPropertyName("lose")]
    public int? Lose { get; set; }
    [JsonPropertyName("points")]
    public int? Points { get; set; }

    public Player(string nickName,
                  string description,
                  string? imageName = null,
                  int? id = null,
                  int? groupNumber = null,
                  int? won = null,
                  int? lose = null,
                  int? points = null)
    {
        Id = id;
        NickName = nickName;
        ImageName = imageName;
        Description = description;
        GroupNumber = groupNumber;
        Won = won;
        Lose = lose;
        Points = points;
    }
}
