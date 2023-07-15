using System.Globalization;
using System.Text.Json;
using Temporary;
using YApi;
using YApiModel.Models;

var client = new YClient("token");

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

Console.WriteLine(JsonSerializer.Serialize(inputGroups));

client.GroupFillAsync(inputGroups).Wait();
var players = client.PlayersGetAsync().Result;
var player1 = players.First(p => p.Id == 1);
var player2 = players.First(p => p.Id == 2);

var groups = await client.GroupGetGamesAsync();

Console.WriteLine("________________");

foreach (var g in groups)
{
    Console.WriteLine();
    Console.WriteLine($"Group {g.Group}");
    Console.WriteLine($"PlayerId {g.PlayerId}");
    Console.WriteLine();
}

Console.WriteLine(JsonSerializer.Serialize(groups));

Console.WriteLine("________________");

var group = groups.First(g => g.Group == player1.GroupNumber);
