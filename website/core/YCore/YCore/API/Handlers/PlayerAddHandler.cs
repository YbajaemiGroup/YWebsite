using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;
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
                var db = DatabaseInteractor.Instance();
                var processedPlayers = new List<YDatabase.Models.Player>();
                var dbPlayers = db.GetPlayers();
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
                foreach (var player in players.Where(p => p.Id == null))
                {
                    processedPlayers.Add(db.InsertPlayer(new YDatabase.Models.Player()
                    {
                        Nickname = player.NickName,
                        ImageId = GetImage(player.ImageName, db)?.Id,
                        Descr = player.Description,
                        GroupNumber = player.GroupNumber,
                        Won = player.Won,
                        Lose = player.Lose,
                        Points = player.Points
                    }).Result);
                }
                return GetResponseSender(processedPlayers
                    .Select(p => new YApiModel.Models.Player(
                        p.Nickname,
                        p.Descr,
                        p.Image?.ImageName,
                        p.Id,
                        p.GroupNumber,
                        p.Won,
                        p.Lose,
                        p.Points)));
            }
            catch (AggregateException e)
            {
                var exception = e.InnerExceptions.FirstOrDefault(ex => ex.InnerException is PostgresException);
                if (exception == null || exception?.InnerException is not PostgresException postgresException)
                {
                    Logger.Log(LogSeverity.Warning, nameof(PlayerAddHandler), "Error occured in player.add handler.", e);
                    CoreException = new UnknownInnerException();
                    return GetResponseSender(null);
                }
                Logger.Log(LogSeverity.Warning, nameof(PlayerAddHandler), "Entity update throws exception.");
                CoreException = new DatabaseException(postgresException.SqlState ?? "No error code found", postgresException);
                return GetResponseSender(null);
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
