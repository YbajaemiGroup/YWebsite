using System.Text.Json;
using YApi;
using YApiModel.Models;

var client = new YClient("token");
var player = new Player(
    "player's nickname",
    "no description");
var dbPlayer = client.PlayersAddOrUpdateAsync(new() { player }).Result.First();
dbPlayer.Won = 100;
dbPlayer.Lose = 100;
dbPlayer.Description = "some kind of a new description";
dbPlayer = client.PlayersAddOrUpdateAsync(new() { dbPlayer }).Result.First();
Console.WriteLine(JsonSerializer.Serialize(dbPlayer));

client.PlayerDelete(dbPlayer.Id ?? -1).Wait();

Console.ReadLine();