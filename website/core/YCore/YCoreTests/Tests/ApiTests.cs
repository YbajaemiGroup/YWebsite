using YApi;
using YApiModel.Models;
using YCore.Data;
using YDatabase.Models;

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
            var player = new YApiModel.Models.Player(
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

        [Fact]
        public void LinksCRUDTest()
        {
            var link = new YApiModel.Models.Link(
                "somelink",
                "someshee");
            link = client.AddLinksAsync(new() { link }).Result.FirstOrDefault();
            Assert.NotNull(link);
            Assert.NotNull(link.Id);
            Assert.Equal("somelink", link.LinkUrl);
            Assert.Equal("someshee", link.Description);

            var links = client.GetLinksAsync().Result;
            Assert.Contains(links, l => l.Id == link.Id);
            client.DeleteLinksAsync(link.Id ?? -1).Wait();
            links = client.GetLinksAsync().Result;
            Assert.DoesNotContain(links, l => l.Id == link.Id);

            var player = client.PlayersGetAsync().Result.FirstOrDefault();
            Assert.NotNull(player);
            Assert.NotNull(player.Id);

            link.PlayerId = player.Id;
            link = client.AddLinksAsync(new() { link }).Result.FirstOrDefault();
            Assert.NotNull(link);
            Assert.NotNull(link.Id);
            links = client.GetLinksAsync().Result;
            Assert.Contains(links, l => l.Id == link.Id);
            client.DeleteLinksAsync(link.Id ?? -1).Wait();
        }

        [Fact]
        public void GroupsGetFillTest()
        {
            var inputGroups = new List<GroupFillData>()
            {
                new()
                {
                    Group = 1,
                    PlayerId = 1
                },
                new()
                {
                    Group = 1,
                    PlayerId = 2
                }
            };
            client.GroupFillAsync(inputGroups).Wait();
            var players = client.PlayersGetAsync().Result;
            var player1 = players.First(p => p.Id == 1);
            var player2 = players.First(p => p.Id == 2);
            Assert.Equal(1, player1.GroupNumber);
            Assert.Equal(1, player2.GroupNumber);

            Assert.Equal(player1.GroupNumber, player2.GroupNumber);
            var group = client.GroupGetGames().Result.First(g => g.Group == player1.GroupNumber);
            Assert.Contains(group.PlayerId, p => p == player1.Id);
            Assert.Contains(group.PlayerId, p => p == player2.Id);
        }

        [Fact]
        public void BracketTest()
        {
            var rounds = new List<Round>()
            {
                new()
                {
                    RoundNumber = 1,
                    IsUpper = true,
                    Games = new()
                    {
                        new()
                        {
                            Player1Id = 1,
                            Player2Id = 2,
                            WinnerId = null
                        }
                    }
                },
                new()
                {
                    RoundNumber = 2,
                    IsUpper = true,
                    Games = new()
                    {
                        new()
                        {
                            Player1Id = 1,
                            Player2Id = 2,
                            WinnerId = 1
                        }
                    }
                }
            };
            client.SetBracketAsync(rounds).Wait();
            var rounds1 = client.GetBracketAsync().Result;
            Assert.NotNull(rounds1);
            Assert.Equal(2, rounds1.Count);
        }
    }
}
