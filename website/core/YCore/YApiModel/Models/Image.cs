using System.Text.Json.Serialization;

namespace YApiModel.Models
{
    public class Image
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("image_name")]
        public string ImageName { get; set; }

        public Image(int id, string imageName)
        {
            Id = id;
            ImageName = imageName;
        }
    }
}
