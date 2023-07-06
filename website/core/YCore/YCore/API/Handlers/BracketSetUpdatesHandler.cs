using YApiModel;
using YApiModel.Models;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class BracketSetUpdatesHandler : Handler, IHandler
    {
        private readonly List<Round> bracket;

        public BracketSetUpdatesHandler(List<Round> bracket)
        {
            this.bracket = bracket;
        }

        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            int uid = db.GetLastGamesUpdationId() + 1;
            var tasks = new List<YDatabase.Models.Game>();
            foreach (var round in bracket)
            {
                foreach (var game in round.Games)
                {
                    tasks.Add(db.InsertGame(new()
                    {
                        UpdationId = uid,
                        Player1 = game.Player1Id,
                        Player2 = game.Player2Id,
                        Round = round.RoundNumber,
                        IsUpper = round.IsUpper,
                        IsGroup = false,
                        Winner = game.WinnerId
                    }).Result);
                }
            }
            //Task.WaitAll(tasks.ToArray());
            return GetResponseSender(null);
        }
    }
}
