using System.Text.Json;
using YApi;
using YApiModel.Models;

var client = new YClient("token");

var pl = await client.PlayersAddOrUpdateAsync(new()
{
    new Player("player123", "123")
});

foreach (var p in pl)
{
    Console.WriteLine(JsonSerializer.Serialize(p));
}
Console.ReadLine();

pl = await client.PlayersAddOrUpdateAsync(new()
{
    new Player("player123", "123", id: pl.First().Id, imageName: "img1.png")
});

foreach (var p in pl)
{
    Console.WriteLine(JsonSerializer.Serialize(p));
}
