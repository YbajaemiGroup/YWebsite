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

        public async Task<List<Player>> UpdatePlayersAsync(List<Player> players)
        {
            return await _client.PlayersAddOrUpdateAsync(players);
        }

        public async Task DeletePlayer(int playerId)
        {
            await _client.PlayerDelete(playerId);
        }

        public void DownloadImage(string imageName, string imagesDirectory)
        {
            using var dbImage = _client.GetImage(imageName, YApiModel.ImageType.Players).Result;
            using var localImage = File.OpenWrite($"{imagesDirectory}\\{imageName}");
            dbImage.CopyTo(localImage);
            localImage.Flush();
            dbImage.Flush();
        }
    }
}
