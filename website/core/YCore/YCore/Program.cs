// See https://aka.ms/new-console-template for more information
using YCore;
using YCore.API;
using YCore.Data;
using YCore.Data.OS;

Console.WriteLine("Hello, World!");

var configuration = ConfigurationLoader.Load("H:\\YBAJAEMI\\cons\\config.json");
DatabaseInteractor.LoadConnectionString(configuration.DbConnectionString);
var core = new Core(configuration);
var apiHandler = new ApiHandler(configuration);
var httpHandler = new HttpHandler(configuration);
core.RequestReceived += httpHandler.ExecuteHandler;
core.RequestReceived += apiHandler.ExecuteHandler;
core.Start();
Console.ReadLine();
core.Stop();