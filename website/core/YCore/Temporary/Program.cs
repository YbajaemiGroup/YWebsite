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
