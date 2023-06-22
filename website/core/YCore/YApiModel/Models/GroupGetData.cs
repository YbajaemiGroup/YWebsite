using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
