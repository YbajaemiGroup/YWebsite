using System.Text.Json.Serialization;

namespace YCore.Data
{
    public class Configuration
    {
        [JsonPropertyName("database_connection_string")]
        public string DbConnectionString { get; private set; }

        [JsonPropertyName("images_location")]
        public string ImagesLocation { get; private set; }

        [JsonPropertyName("frontend_location")]
        public string FrontendLocation { get; private set; }

        [JsonPropertyName("staff_images_location")]
        public string StaffImagesLocation { get; private set; }

        [JsonPropertyName("api_listen_addresses")]
        public List<string> ApiListenAddresses { get; private set; }        
        
        [JsonPropertyName("http_listen_addresses")]
        public List<string> HttpListenAddresses { get; private set; }

        public Configuration(string dbConnectionString, string imagesLocation, string frontendLocation, string staffImagesLocation, List<string> apiListenAddresses, List<string> httpListenAddresses)
        {
            DbConnectionString = dbConnectionString;
            ImagesLocation = imagesLocation;
            FrontendLocation = frontendLocation;
            StaffImagesLocation = staffImagesLocation;
            ApiListenAddresses = apiListenAddresses;
            HttpListenAddresses = httpListenAddresses;
        }
    }
}
