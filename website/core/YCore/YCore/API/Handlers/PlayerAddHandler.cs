using YCore.API.IO.Exceptions;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class PlayerAddHandler : Handler, IHandler
    {
        private List<YApiModel.Models.Player> players;

        public PlayerAddHandler(List<YApiModel.Models.Player> players)
        {
            this.players = players;
        }

        private static YDatabase.Models.Image? GetImage(string? imageName, DatabaseInteractor db)
        {
            if (imageName == null)
            {
                return null;
            }
            YDatabase.Models.Image? image;
            try
            {
                image = db.GetImage(imageName);
            }
            catch (Exception)
            {
                image = null;
            }
            return image;
        }

        public IResponseSender GetResponseSender()
        {
            if (players.Count == 0)
            {
                return GetResponseSender(null);
            }
            try
            {
                var newPlayers = players.Where(p => p.Id == null).ToList();
                var db = DatabaseInteractor.Instance();
                var tasks = new List<Task<YDatabase.Models.Player>>();
                foreach (var player in newPlayers)
                {
                    tasks.Add(db.InsertPlayer(new YDatabase.Models.Player()
                    {
                        Nickname = player.NickName,
                        ImageId = GetImage(player.ImageName, db)?.Id,
                        Descr = player.Description,
                        GroupNumber = player.GroupNumber,
                        Won = player.Won,
                        Lose = player.Lose,
                        Points = player.Points
                    }));
                }
                var dbPlayers = db.GetPlayers();
                var processedPlayers = new List<YDatabase.Models.Player>();
                foreach (var player in players.Where(p => p.Id != null))
                {
                    if (!dbPlayers.Any(p => p.Id == player.Id))
                    {
                        continue;
                    }
                    processedPlayers.Add(db.UpdatePlayer(new YDatabase.Models.Player()
                    {
                        Id = player.Id.Value,
                        Nickname = player.NickName,
                        Descr = player.Description,
                        GroupNumber = player.GroupNumber,
                        Won = player.Won,
                        Lose = player.Lose,
                        Points = player.Points
                    }));
                }
                processedPlayers.AddRange(tasks.Select(t => t.Result));
                return GetResponseSender(processedPlayers.Select(p =>
                    new YApiModel.Models.Player(
                        nickName: p.Nickname,
                        description: p.Descr,
                        imageName: p.Image?.ImageName,
                        id: p.Id,
                        groupNumber: p.GroupNumber,
                        won: p.Won,
                        lose: p.Lose,
                        points: p.Points
                        )));
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Warning, nameof(PlayerAddHandler), "Error occured in player.add handler.", e);
                CoreException = new UnknownInnerException();
                return GetResponseSender(null);
            }
        }
    }
}
