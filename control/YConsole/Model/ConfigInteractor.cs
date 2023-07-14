using System;
using System.IO;
using System.Xml;
using YApi;

namespace YConsole.Model
{
    public class ConfigInteractor : IConfigInteractor
    {
#if DEBUG
        private const string CONFIG_PATH = "E:\\MyProgs\\ybajaemi\\config\\ConsoleConfig\\";
#else
        private const string CONFIG_PATH = ".\\Conf\\";
#endif
        private const string CONFIG_FILE = "Config.xml";
        private const string TOKEN_FILE = "Token.txt";

        private const string TOKEN_FILE_PATH = CONFIG_PATH + TOKEN_FILE;
        private const string CONFIG_FILE_PATH = CONFIG_PATH + CONFIG_FILE;

        public string GetToken()
        {
            var file = File.OpenRead(TOKEN_FILE_PATH);
            using var fileReader = new StreamReader(file);
            string token = fileReader.ReadToEnd();
            ArgumentException.ThrowIfNullOrEmpty(token, "token");
            return token;
        }

        public string GetImagesLocation()
        {
            var config = new XmlDocument();
            config.Load(CONFIG_FILE_PATH);
            var root = config.DocumentElement;
            if (root == null)
                throw new NullReferenceException("No root in config xml found.");
            var images = root.GetElementsByTagName("images_location");
            var image = images[0];
            if (image == null)
                throw new NullReferenceException("No images_location node found.");
            return image.InnerText;
        }
    }
}
