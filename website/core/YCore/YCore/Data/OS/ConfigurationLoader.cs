using System.Text.Json;

namespace YCore.Data.OS
{
    internal static class ConfigurationLoader
    {
        public static Configuration Load(string path)
        {
            var reader = new StreamReader(path);
            return JsonSerializer.Deserialize<Configuration>(reader.ReadToEnd())
                ?? throw new ArgumentNullException("Configuration", "Json configuration deserealizer return null.");
        }
    }
}
