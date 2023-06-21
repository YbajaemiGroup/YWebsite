using YCore.Crypto;
using YDatabase;
using YDatabase.Models;

namespace YCore.Data;

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
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Connection string loaded.");
    }

    public async Task<Game> InsertGame(Game game)
    {
        var g = await Context.Games.AddAsync(game);
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Game added to database.");
        await CommitAsync();
        return g.Entity;
    }

    public Game GetGame(int id)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Game selected from database.");
        return Context.Games.First(g => g.Id == id);
    }

    public List<Game> GetGames()
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "All games returned from database.");
        return Context.Games.ToList();
    }

    public Game UpdateGame(Game game)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Game updated.");
        return Context.Games.Update(game).Entity;
    }

    public Player GetPlayer(int id)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Player selected from database.");
        return Context.Players.First(p => p.Id == id);
    }

    public List<Player> GetPlayers()
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "All players selected from database.");
        return Context.Players.ToList();
    }

    public async Task<Player> InsertPlayer(Player player)
    {
        var p = await Context.Players.AddAsync(player);
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Player added to database.");
        await CommitAsync();
        return p.Entity;
    }

    public Player UpdatePlayer(Player player)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Player updated.");
        return Context.Players.Update(player).Entity;
    }

    public async Task<Link> InsertLink(Link link)
    {
        var l = await Context.Links.AddAsync(link);
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Link added in database.");
        await CommitAsync();
        return l.Entity;
    }

    public Link GetLink(int id)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Link selected from database.");
        return Context.Links.First(l => l.Id == id);
    }

    public Link GetLinkByPlayerId(int playerId)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Link selected from database by playerId.");
        return Context.Links.First(l => l.Player == playerId);
    }

    public bool DeleteLink(int id)
    {
        try
        {
            var link = GetLink(id);
            Context.Links.Remove(link);
            Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Link deleted from database.");
            return true;
        }
        catch (ArgumentNullException)
        {
            return false;
        }
    }

    public List<Link> GetLinks()
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "All links selected from database.");
        return Context.Links.ToList();
    }

    public List<Link> GetPlayerLinks(int playerId)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Link selected from database by playerId");
        return Context.Links.Where(l => l.Player == playerId).ToList();
    }

    public async Task<Image> InsertImage(Image image)
    {
        var i = await Context.Images.AddAsync(image);
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Image added in database.");
        await CommitAsync();
        return i.Entity;
    }

    public Image GetImage(int id)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Image selected from database.");
        return Context.Images.First(i => i.Id == id);
    }

    public List<Image> GetStaffImages()
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Staff images selected from database.");
        return Context.Images.Where(i => i.IsStaff).ToList();
    }

    public Image GetImageByPlayerId(int playerId)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Image selected from database by playerId.");
        var player = GetPlayer(playerId);
        return Context.Images.First(i => i.Id == player.ImageId);
    }

    public bool DeleteImage(int id)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Delete image from database.");
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
        Logger.Log(LogSeverity.Info, nameof(DatabaseInteractor), "Token added in database.");
        string hash = YCoreCrypto.GetHash(key);
        await Context.Tokens.AddAsync(new Token() { Hash = hash });
        await CommitAsync();
    }

    public bool DeleteToken(string key)
    {
        Logger.Log(LogSeverity.Info, nameof(DatabaseInteractor), "Token deleted from database.");
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

    /// <summary>
    /// True if OK
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ValidateToken(string key)
    {
        string hash = YCoreCrypto.GetHash(key);
        bool validated = Context.Tokens.Any(t => t.Hash == hash);
        Logger.Log(LogSeverity.Info, nameof(DatabaseInteractor), $"Token {(validated ? "" : "not")} validated.");
        return validated;
    }

    public async Task CommitAsync()
    {
        await Context.SaveChangesAsync();
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Changes saved.");
    }

    public async Task WrileLog(LogSeverity logSeverity, string source, string message, Exception? exception = null)
    {
        await Context.Logs.AddAsync(new()
        {
            DateTime = DateTime.UtcNow,
            Severety = logSeverity.ToString(),
            Source = source,
            Message = message,
            Exception = exception?.ToString()
        });
    }
}
