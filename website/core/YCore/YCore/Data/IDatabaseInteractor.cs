using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDatabase.Models;

namespace YCore.Data
{
    public interface IDatabaseInteractor
    {
        Game InsertGame(Game game);
        Game GetGame(int id);
        List<Game> GetGames();
        Game UpdateGame(Game game);

        Player GetPlayer(int id);
        List<Player> GetPlayers();
        Player InsertPlayer(Player player);
        Player UpdatePlayer(Player player);

        Link InsertLink(Link link);
        Link GetLink(int id);
        Link GetLinkByPlayerId(int playerId);
        bool DeleteLink(int id);
        List<Link> GetLinks();
        List<Link> GetPlayerLinks(int playerId);

        Image InsertImage(Image image);
        Image GetImage(int id);
        List<Image> GetStaffImages();
        Image GetImageByPlayerId(int playerId);
        bool DeleteImage(int id);

        bool CreateToken(string key);
        bool DeleteToken(string key);
        bool ValidateToken(string key);
    }
}
