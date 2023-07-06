using System.Text.Json.Serialization;

namespace YApiModel.Models
{
    public class GroupGetData
    {
        [JsonPropertyName("group")]
        public int Group { get; set; }
        [JsonPropertyName("players")]
        public List<int> PlayerId { get; set; } = new();
    }
}
