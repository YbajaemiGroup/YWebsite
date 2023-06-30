using YApi;
using YCore.Data;

namespace YCoreTests.Tests
{
    [Collection("ServerCoreCollection")]
    public class ApiTests
    {
        private readonly YClient client;

        public ApiTests(ServerCoreFixture serverCoreFixture)
        {
            client = serverCoreFixture.Client;
        }

        [Fact]
        public void TokenCreateAndDeleteTest()
        {
            string newToken = "new_token";
            client.CreateToken(newToken).Wait();
            var db = DatabaseInteractor.Instance();
            Assert.True(db.ValidateToken(newToken));
            client.DeleteToken(newToken).Wait();
            Assert.False(db.ValidateToken(newToken));
        }

        [Fact]
        public void ImagesLoadAndGetTest()
        {
            string imageName = "image.png";
            using var ms = new MemoryStream();
            using var fs = File.OpenRead("E:\\MyProgs\\ybajaemi\\config\\1.png");
            fs.CopyTo(ms);
            var imageBytes = ms.ToArray();
            var image = client.LoadImageAsync(imageName, imageBytes).Result;
            Assert.Equal(imageName, image.ImageName);
            var imageBytes1 = client.GetImage(imageName, YApiModel.ImageType.Players).Result;
            Assert.Equal(imageBytes.Length, imageBytes1.Length);
        }


        [Fact]
        public void CreateTokenTest()
        {
            DatabaseInteractor.Instance().CreateToken("token");
        }
    }
}
