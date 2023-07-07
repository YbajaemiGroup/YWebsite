using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YApi;
using YCore.Data;

namespace YCoreTests
{
    public class ConfigCreatorTest
    {
        /// <summary>
        /// Создает пример конфигурационного файла
        /// </summary>
        [Fact]
        public void CreatingConnfigTest()
        {
            var config = new Configuration("dbconstr", "imagesLoc", "staffImages", "frontendLocation", new List<string>() { "str1", "str2" }, new List<string>() { "str1", "str2" });
            string json = JsonSerializer.Serialize(config);
            FileStream fileStream = File.Create("E:\\MyProgs\\ybajaemi\\config\\config.json");
            using StreamWriter writer = new(fileStream);
            writer.Write(json);
            writer.Flush();
        }
    }
}
