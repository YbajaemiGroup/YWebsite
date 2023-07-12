using System.Data;
using System.Text.Json;
using Temporary;
using YApi;
using YApiModel.Models;


var cl = new Class1();
Console.WriteLine("Start.");
var intGetting = cl.GetInt();
await foreach (var i in cl.GetIntsAsync())
{
    Console.WriteLine(i);
}
Console.WriteLine("Finish");