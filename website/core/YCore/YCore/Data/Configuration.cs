using System.Text.Json.Serialization;

namespace YCore.Data
{
    internal class Configuration
    {
        [JsonPropertyName("database_connection_string")]
        public string DbConnectionString { get; private set; }

        [JsonPropertyName("images_location")]
        public string ImagesLocation { get; private set; }
        [JsonPropertyName("staff_images_location")]
        public string StaffImagesLocation { get; private set; }

        [JsonPropertyName("accepted_listen_addresses")]
        public List<string> ListenAddresses { get; private set; }

        
        public Configuration(string dbConnectionString, string imagesLocation, string staffImagesLocation, List<string> listenAddresses)
        {
            DbConnectionString = dbConnectionString;
            ImagesLocation = imagesLocation;
            StaffImagesLocation = staffImagesLocation;
            ListenAddresses = listenAddresses;
        }
    }
}
