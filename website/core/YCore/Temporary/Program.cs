using System.Data;
using System.Text.Json;
using YApi;
using YApiModel.Models;

var client = new YClient("token");
//var rounds = new List<Round>()
//            {
//                new()
//                {
//                    RoundNumber = 1,
//                    IsUpper = true,
//                    Games = new()
//                    {
//                        new()
//                        {
//                            Row = 1,
//                            Player1Id = 1,
//                            Player2Id = 2,
//                            WinnerId = null
//                        }
//                    }
//                },
//                new()
//                {
//                    RoundNumber = 2,
//                    IsUpper = true,
//                    Games = new()
//                    {
//                        new()
//                        {
//                            Row = 1,
//                            Player1Id = 1,
//                            Player2Id = 2,
//                            WinnerId = 1
//                        }
//                    }
//                }
//            };
var bracket = client.GetBracketAsync().Result;

Console.WriteLine(JsonSerializer.Serialize(bracket));

Console.ReadLine();