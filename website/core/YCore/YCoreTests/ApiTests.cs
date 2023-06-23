﻿using System.Text.Json;
using YApiModel.Models;
using YCore.API.Handlers;
using YCore.Data;
using YDatabase.Models;

namespace YCoreTests
{
    public class ApiTests
    {

        [Fact]
        public async void BracketSetUpdatesHandlerTest()
        {
            DatabaseInteractor.LoadConnectionString(Constants.CONNECTION_STRING);
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
                        new YApiModel.Models.Game()
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

        [Fact]
        public void BracketGetUpdatesHandlerTest()
        {
            DatabaseInteractor.LoadConnectionString(Constants.CONNECTION_STRING);
            var resp = new BracketGetUpdatesHandler().ProcessRequest();
        }
    }
}
