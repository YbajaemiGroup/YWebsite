using System.Text.Json;
using YApi;
using YApiModel.Models;

var client = new YClient("token");
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

Console.ReadLine();