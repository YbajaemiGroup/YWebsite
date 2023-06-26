using YDatabase.Models;

namespace YCore.Data;

public interface IDatabaseInteractor
{
    Task<Game> InsertGame(Game game);
    Game GetGame(int id);
    List<Game> GetGames();
    Game UpdateGame(Game game);
    List<Game> GetGroupGames();
    List<Game> GetPlayOffGames();

    Player GetPlayer(int id);
    List<Player> GetPlayers();
    Task<Player> InsertPlayer(Player player);
    Player UpdatePlayer(Player player);

    Task<Link> InsertLink(Link link);
    Link GetLink(int id);
    Link GetLinkByPlayerId(int playerId);
    bool DeleteLink(int id);
    List<Link> GetLinks();
    List<Link> GetPlayerLinks(int playerId);

    Task<Image> InsertImage(Image image);
    Image GetImage(int id);
    List<Image> GetStaffImages();
    Image GetImageByPlayerId(int playerId);
    public Image GetImage(string name);

    void CreateToken(string key);
    bool DeleteToken(string key);
    bool ValidateToken(string key);

    Task WrileLog(LogSeverity logSeverity, string source, string message, Exception? exception = null);

    Task CommitAsync();
}
