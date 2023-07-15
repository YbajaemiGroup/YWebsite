using System.Globalization;
using System.Text.Json;
using Temporary;
using YApi;
using YApiModel.Models;

var client = new YClient("token");

foreach (var image in client.GetImagesList().Result)
{
    Console.WriteLine(JsonSerializer.Serialize(image));
}