using YApi;
using YApiModel.Models;
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

        [Fact]
        public void PlayersCRUDTest()
        {
            var player = new Player(
                "player's nickname",
                "no description");
            var dbPlayer = client.PlayersAddOrUpdateAsync(new() { player }).Result.First();
            Assert.NotNull(dbPlayer.Id);
            Assert.Equal(player.NickName, dbPlayer.NickName);
            Assert.Equal(player.Description, dbPlayer.Description);
            dbPlayer.Won = 100;
            dbPlayer.Lose = 100;
            dbPlayer.Description = "some kind of a new description";
            dbPlayer = client.PlayersAddOrUpdateAsync(new() { dbPlayer }).Result.First();
            Assert.NotNull(dbPlayer.Id);
            Assert.Equal(player.NickName, dbPlayer.NickName);
            Assert.Equal("some kind of a new description", dbPlayer.Description);
            Assert.Equal(100, dbPlayer.Won);
            Assert.Equal(100, dbPlayer.Lose);

            Assert.Contains(client.PlayersGetAsync().Result, p => p.NickName == "player's nickname");
            client.PlayerDelete(dbPlayer.Id ?? -1).Wait();
            Assert.DoesNotContain(client.PlayersGetAsync().Result, p => p.NickName == "player's nickname");
        }
    }
}
