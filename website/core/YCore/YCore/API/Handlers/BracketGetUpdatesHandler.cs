using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApiModel;
using YApiModel.Models;
using YCore.API.IO.Exceptions;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class BracketGetUpdatesHandler : Handler, IHandler
    {
#warning протестировать на то, чтобы не было групповых игр тут
        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            var games = db.GetPlayOffGames();
            var responseData = new List<Round>();

            foreach (var game in games)
            {
                if (!responseData.Any(r => r.RoundNumber == game.Round))
                {
                    if (game.Round == null)
                    {
                        CoreException = new UnknownInnerException();
                        Logger.Log(LogSeverity.Error, nameof(BracketGetUpdatesHandler), "Error raised.", 
                            new ArgumentNullException(nameof(game.Round), "Каковотохуя null, хотя GetPlayOffGames должно отсеять это все."));
                        break;
                    }
                    if (game.IsUpper == null)
                    {
                        CoreException = new UnknownInnerException();
                        Logger.Log(LogSeverity.Error, nameof(BracketGetUpdatesHandler), "Error raised.", 
                            new ArgumentNullException(nameof(game.IsUpper), "Каковотохуя null, хотя GetPlayOffGames должно отсеять это все."));
                        break;
                    }
                    responseData.Add(new Round()
                    {
                        RoundNumber = game.Round.Value,
                        IsUpper = game.IsUpper.Value,
                        Games = new()
                        {
                            new()
                            {
                                Player1Id = game.Player1,
                                Player2Id = game.Player2,
                                WinnerId = game.Winner
                            }
                        }
                    });
                }
                else
                {
                    responseData.First(r => r.RoundNumber == game.Round)
                        .Games.Add(new()
                        {
                            Player1Id = game.Player1,
                            Player2Id = game.Player2,
                            WinnerId = game.Winner
                        });
                }
            }

            return GetResponseSender(responseData);
        }
    }
}
