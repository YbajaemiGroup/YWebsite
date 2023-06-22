using System.Text.Json;
using YApiModel;
using YCore.Data;

namespace YCoreTests
{
    public class HandlerFactoryTest
    {
        // Эвивалент HandlerFactory GetData
        [Fact]
        public void GetDataTest()
        {
            var conf = new Configuration("dbconstr", "imagesLoc", "staffImages", new List<string>()
            {
                "str1",
                "str2"
            });
            string json = """
                {"data": [
                  {
                  "database_connection_string": "dbconstr",
                  "images_location": "imagesLoc",
                  "staff_images_location": "staffImages",
                  "accepted_listen_addresses": [
                    "str1",
                    "str2"
                  ]
                }]}
                """;
            var request = JsonSerializer.Deserialize<Request>(json);
            Assert.True(request != null);
            var data = JsonSerializer.Deserialize<List<Configuration>>((JsonElement)request.Data);
            Assert.True(data != null);
            Assert.Equivalent(conf, data[0]);
        }
    }
}
