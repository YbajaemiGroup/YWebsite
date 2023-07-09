using Microsoft.EntityFrameworkCore;
using YCore.Crypto;
using YDatabase;
using YDatabase.Models;

namespace YCore.Data;

/// <summary>
/// Need LoadConnectionString before creating Instance
/// </summary>
public class DatabaseInteractor
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

    public int GetLastGamesUpdationId()
    {
        try
        {
            return Context.Games.Where(g => g.UpdationId != null)
                .Max(g => g.UpdationId) ?? 0;
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
        catch (Exception e)
        {
            Logger.Log(LogSeverity.Error, nameof(DatabaseInteractor), "Error occured while taking last updation id.", e);
            return 0;
        }
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

    public async Task DeleteGame(Game game)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Game deleted.");
        Context.Games.Remove(game);
        await CommitAsync();
    }

    public async Task DeleteGames(IEnumerable<Game> games)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Games deleted.");
        Context.Games.RemoveRange(games);
        await CommitAsync();
    }

    public List<Game> GetGroupGames()
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Group games selected.");
        return Context.Games.Where(g => g.IsGroup).ToList();
    }

    public List<Game> GetPlayOffGames()
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Play off games selected.");
        int uid = GetLastGamesUpdationId();
        return Context.Games
            .Where(g => !g.IsGroup)
            .Where(g => g.UpdationId == uid)
            .ToList();
    }

    public Player GetPlayer(int id)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Player selected from database.");
        return Context.Players
            .AsQueryable()
            .Include(p => p.GamePlayer1Navigations)
            .Include(p => p.GamePlayer2Navigations)
            .Include(p => p.Image)
            .First(p => p.Id == id);
    }

    public List<Player> GetPlayers()
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "All players selected from database.");
        return Context.Players
            .AsQueryable()
            .Include(p => p.GamePlayer1Navigations)
            .Include(p => p.GamePlayer2Navigations)
            .Include(p => p.Image)
            .ToList();
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
        var dbPlayer = Context.Players.First(p => p.Id == player.Id);
        dbPlayer.Copy(player);
        CommitAsync().Wait();
        return dbPlayer;
    }

    public async void DeletePlayer(Player player)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Player deleted.");
        Context.Players.Remove(player);
        await CommitAsync();
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
        return Context.Links
            .AsQueryable()
            .Include(l => l.PlayerNavigation)
            .First(l => l.Id == id);
    }

    public Link GetLinkByPlayerId(int playerId)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Link selected from database by playerId.");
        return Context.Links
            .AsQueryable()
            .Include(l => l.PlayerNavigation)
            .First(l => l.Player == playerId);
    }

    public async Task<bool> DeleteLink(int id)
    {
        try
        {
            var link = GetLink(id);
            Context.Links.Remove(link);
            await CommitAsync();
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
        return Context.Links
            .AsQueryable()
            .Include(l => l.PlayerNavigation)
            .ToList();
    }

    public List<Link> GetPlayerLinks(int playerId)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Link selected from database by playerId");
        return Context.Links
            .AsQueryable()
            .Include(l => l.PlayerNavigation)
            .Where(l => l.Player == playerId).ToList();
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

    public Image GetImage(string name)
    {
        Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Image selected by name.");
        return Context.Images
            .AsQueryable()
            .Include(i => i.Players)
            .First(i => i.ImageName == name);
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
        return Context.Images
            .AsQueryable()
            .Include(i => i.Players)
            .First(i => i.Id == player.ImageId);
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
            CommitAsync().Wait();
            return true;
        }
        catch (Exception e) when (e is InvalidOperationException or ArgumentNullException)
        {
            return false;
        }
        catch (Exception e)
        {
            Logger.Log(LogSeverity.Warning, nameof(DatabaseInteractor), "Exception occured while token deleting.", e);
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
        bool validated = Context.Tokens.AsQueryable().Any(t => t.Hash == hash);
        Logger.Log(LogSeverity.Info, nameof(DatabaseInteractor), $"Token {(validated ? "" : "not")} validated.");
        return validated;
    }

    public async Task CommitAsync(bool isdb = false)
    {
        if (isdb)
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        try
        {
            await Context.SaveChangesAsync();
            Logger.Log(LogSeverity.Debug, nameof(DatabaseInteractor), "Changes saved.");
        }
        catch (Exception e)
        {
            Logger.Log(LogSeverity.Error, nameof(DatabaseInteractor), "Error occured while saving changes in database.", e);
            throw;
        }
    }

    public async Task WrileLog(LogSeverity logSeverity, string source, string message, Exception? exception = null)
    {
        await Context.Logs.AddAsync(new()
        {
            DateTime = DateTime.Now,
            Severety = logSeverity.ToString(),
            Source = source,
            Message = message,
            Exception = exception?.ToString()
        });
        await CommitAsync(true);
    }
}
