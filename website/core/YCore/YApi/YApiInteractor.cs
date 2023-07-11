using System.Diagnostics.Metrics;
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

        public async Task DownloadImageAsync(string imageName, string imagesDirectory)
        {
            using var dbImage = await _client.GetImage(imageName, YApiModel.ImageType.Players);
            using var localImage = File.OpenWrite($"{imagesDirectory}\\{imageName}");
            await dbImage.CopyToAsync(localImage);
            await localImage.FlushAsync();
            await dbImage.FlushAsync();
        }

        public async Task DownloadAllImagesAsync(string imageDirectory)
        {
            var players = await _client.PlayersGetAsync();
            var downloadingImages = new List<Task>();
            foreach (var image in players.Select(p => p.ImageName))
            {
                if (image != null)
                {
                    downloadingImages.Add(DownloadImageAsync(image, imageDirectory));
                }
            }
            Task.WaitAll(downloadingImages.ToArray());
        }
    }
}
