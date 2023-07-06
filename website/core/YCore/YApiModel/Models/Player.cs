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

    public override bool Equals(object? obj)
    {
        if (obj is Player player)
        {
            bool equal = NickName == player.NickName &&
               ImageName == player.ImageName &&
               Description == player.Description &&
               GroupNumber == player.GroupNumber &&
               Won == player.Won &&
               Lose == player.Lose &&
               Points == player.Points;
            if (player?.Id != null)
            {
                equal &= player.Id == Id;
            }
            return equal;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, NickName, ImageName, Description, GroupNumber, Won, Lose, Points);
    }
}
