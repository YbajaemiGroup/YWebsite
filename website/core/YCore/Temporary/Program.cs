using System.Text.Json;
using YApi;
using YApiModel.Models;

var client = new YClient("token");
var link = new Link(
    "somelink",
    "someshee");
link = client.AddLinksAsync(new() { link }).Result.FirstOrDefault();

var links = client.GetLinksAsync().Result;

client.DeleteLinksAsync(link.Id ?? -1).Wait();
links = client.GetLinksAsync().Result;


var player = client.PlayersGetAsync().Result.FirstOrDefault();


link.PlayerId = player.Id;
link = client.AddLinksAsync(new() { link }).Result.FirstOrDefault();

links = client.GetLinksAsync().Result;

client.DeleteLinksAsync(link.Id ?? -1).Wait();

Console.ReadLine();