using YApiModel.Models;
using YCore.API.Handlers;
using YCore.Data;

namespace YCoreTests
{
    public class ApiTests
    {
        [Fact]
        public async void BracketSetUpdatesHandlerTest()
        {
            DatabaseInteractor.LoadConnectionString("Host=localhost;Port=5432;Database=ybajaemi;Username=postgres;Password=nioder125;Include Error Detail=true");
            var db = DatabaseInteractor.Instance();
            var player1 = await db.InsertPlayer(new()
            {
                Nickname = "pl1"
            });
            var player2 = await db.InsertPlayer(new()
            {
                Nickname = "pl2"
            });
            var bracket = new List<Round>()
            {
                new Round()
                {
                    RoundNumber = 1,
                    IsUpper = true,
                    Games = new()
                    {
                        new Game()
                        {
                            Player1Id = player1.Id,
                            Player2Id = player2.Id,
                            WinnerId = player1.Id
                        }
                    }
                }
            };
            var bh = new BracketSetUpdatesHandler(bracket);
            bh.ProcessRequest();
        }
    }
}
