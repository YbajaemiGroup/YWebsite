using System.Text.Json;
using YApi;
using YApiModel.Models;

var client = new YClient("token");

client.PlayerDelete(1).Wait();

Console.ReadLine();