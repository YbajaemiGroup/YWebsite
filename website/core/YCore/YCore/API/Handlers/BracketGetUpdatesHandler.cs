﻿using YApiModel.Models;
using YCore.API.IO.Exceptions;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class BracketGetUpdatesHandler : Handler, IHandler
    {
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
                        Games = new()
                        {
                            new()
                            {
                                Row = game.Row,
                                IsUpper = game.IsUpper.Value,
                                Player1Id = game.Player1,
                                Player2Id = game.Player2,
                                WinnerId = game.Winner
                            }
                        }
                    });
                }
                else
                {
                    if (game.IsUpper == null)
                    {
                        CoreException = new UnknownInnerException();
                        Logger.Log(LogSeverity.Error, nameof(BracketGetUpdatesHandler), "Error raised.",
                            new ArgumentNullException(nameof(game.IsUpper), "Каковотохуя null, хотя GetPlayOffGames должно отсеять это все."));
                        break;
                    }
                    responseData.First(r => r.RoundNumber == game.Round)
                        .Games.Add(new()
                        {
                            Row = game.Row,
                            IsUpper = game.IsUpper.Value,
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
