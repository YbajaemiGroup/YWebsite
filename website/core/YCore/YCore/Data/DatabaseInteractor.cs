using YCore.Crypto;
using YDatabase;
using YDatabase.Models;

namespace YCore.Data
{
    /// <summary>
    /// Need LoadConnectionString before creating Instance
    /// </summary>
    internal class DatabaseInteractor : IDatabaseInteractor
    {
        private static DatabaseInteractor? _instance;

        public static DatabaseInteractor Instance()
        {
            if (!_connectionStringLoaded)
            {
                throw new NullReferenceException("Connection string was not loaded");
            }
            return _instance ??= new();
        }

        private static bool _connectionStringLoaded = false;
        private static string _connectionString = string.Empty;

        public YbajaemiContext Context { get; private set; }

        private DatabaseInteractor()
        {
            Context = new YbajaemiContext(_connectionString);
        }

        public static void LoadConnectionString(string connectionString)
        {
            _connectionStringLoaded = true;
            _connectionString = connectionString;
        }

        public async Task<Game> InsertGame(Game game)
        {
            var g = await Context.Games.AddAsync(game);
            await CommitAsync();
            return g.Entity;
        }

        public Game GetGame(int id)
        {
            return Context.Games.First(g => g.Id == id);
        }

        public List<Game> GetGames()
        {
            return Context.Games.ToList();
        }

        public Game UpdateGame(Game game)
        {
            return Context.Games.Update(game).Entity;
        }

        public Player GetPlayer(int id)
        {
            return Context.Players.First(p => p.Id == id);
        }

        public List<Player> GetPlayers()
        {
            return Context.Players.ToList();
        }

        public async Task<Player> InsertPlayer(Player player)
        {
            var p = await Context.Players.AddAsync(player);
            await CommitAsync();
            return p.Entity;
        }

        public Player UpdatePlayer(Player player)
        {
            return Context.Players.Update(player).Entity;
        }

        public async Task<Link> InsertLink(Link link)
        {
            var l = await Context.Links.AddAsync(link);
            await CommitAsync();
            return l.Entity;
        }

        public Link GetLink(int id)
        {
            return Context.Links.First(l => l.Id == id);
        }

        public Link GetLinkByPlayerId(int playerId)
        {
            return Context.Links.First(l => l.Player == playerId);
        }

        public bool DeleteLink(int id)
        {
            try
            {
                var link = GetLink(id);
                Context.Links.Remove(link);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public List<Link> GetLinks()
        {
            return Context.Links.ToList();
        }

        public List<Link> GetPlayerLinks(int playerId)
        {
            return Context.Links.Where(l => l.Player == playerId).ToList();
        }

        public async Task<Image> InsertImage(Image image)
        {
            var i = await Context.Images.AddAsync(image);
            await CommitAsync();
            return i.Entity;
        }

        public Image GetImage(int id)
        {
            return Context.Images.First(i => i.Id == id);
        }

        public List<Image> GetStaffImages()
        {
            return Context.Images.Where(i => i.IsStaff).ToList();
        }

        public Image GetImageByPlayerId(int playerId)
        {
            var player = GetPlayer(playerId);
            return Context.Images.First(i => i.Id == player.ImageId);
        }

        public bool DeleteImage(int id)
        {
            try
            {
                var image = GetImage(id);
                Context.Images.Remove(image);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public async void CreateToken(string key)
        {
            string hash = YCoreCrypto.GetHash(key);
            await Context.Tokens.AddAsync(new Token() { Hash = hash });
            await CommitAsync();
        }

        public bool DeleteToken(string key)
        {
            string hash = YCoreCrypto.GetHash(key);
            try
            {
                var token = Context.Tokens.First(t => t.Hash == hash);
                Context.Tokens.Remove(token);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public bool ValidateToken(string key)
        {
            string hash = YCoreCrypto.GetHash(key);
            return Context.Tokens.Any(t => t.Hash == hash);
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
