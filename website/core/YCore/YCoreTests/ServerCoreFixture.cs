using YApi;
using YCore;
using YCore.API;
using YCore.Data;
using YCore.Data.OS;

namespace YCoreTests
{
    public class ServerCoreFixture : IDisposable
    {
        public Core Core { get; private set; }
        public YClient Client { get; private set; }

        public ServerCoreFixture()
        {
            Client = new("token");
            var configuration = ConfigurationLoader.Load("E:\\MyProgs\\ybajaemi\\config\\config.json");
            DatabaseInteractor.LoadConnectionString(configuration.DbConnectionString);
            Core = new Core(configuration);
            var apiHandler = new ApiHandler(configuration);
            Core.RequestReceived += apiHandler.ExecuteHandler;
            Core.Start();
        }

        public void Dispose()
        {
            Core.Stop();
            GC.SuppressFinalize(this);
        }
    }

    [CollectionDefinition("ServerCoreCollection")]
    public class ServerCoreCollection : ICollectionFixture<ServerCoreFixture>
    {

    }
}
