using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Game InsertGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Game GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public List<Game> GetGames()
        {
            throw new NotImplementedException();
        }

        public Game UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayer(int id)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public Player InsertPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Player UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Link InsertLink(Link link)
        {
            throw new NotImplementedException();
        }

        public Link GetLink(int id)
        {
            throw new NotImplementedException();
        }

        public Link GetLinkByPlayerId(int playerId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLink(int id)
        {
            throw new NotImplementedException();
        }

        public List<Link> GetLinks()
        {
            throw new NotImplementedException();
        }

        public List<Link> GetPlayerLinks(int playerId)
        {
            throw new NotImplementedException();
        }

        public Image InsertImage(Image image)
        {
            throw new NotImplementedException();
        }

        public Image GetImage(int id)
        {
            throw new NotImplementedException();
        }

        public List<Image> GetStaffImages()
        {
            throw new NotImplementedException();
        }

        public Image GetImageByPlayerId(int playerId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteImage(int id)
        {
            throw new NotImplementedException();
        }

        public bool CreateToken(string key)
        {
            throw new NotImplementedException();
        }

        public bool DeleteToken(string key)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(string key)
        {
            throw new NotImplementedException();
        }
    }
}
