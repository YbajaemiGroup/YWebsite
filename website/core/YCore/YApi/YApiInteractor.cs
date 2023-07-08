using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApiModel.Models;

namespace YApi
{
    public class YApiInteractor
    {
        private readonly YClient _client;

        public YApiInteractor(string token)
        {
            _client = new YClient(token);
        }

        public List<Player> GetAllPlayers()
        {
            return _client.PlayersGetAsync().Result;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            return await _client.PlayersGetAsync();
        }
    }
}
