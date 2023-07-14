using System.Diagnostics;
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

        public List<string> DownloadImages(string imageDirectory)
        {
            var players = _client.PlayersGetAsync().Result;
            var imagesNames = new List<string>();
            foreach (var image in players.Select(p => p.ImageName))
            {
                if (image != null)
                {
                    DownloadImage(image, imageDirectory);
                    imagesNames.Add(image);
                }
            }
            return imagesNames;
        }

        public async IAsyncEnumerable<string> DownloadAllImagesAsync(string imageDirectory)
        {
            var players = await _client.PlayersGetAsync();
            foreach (var image in players.Select(p => p.ImageName))
            {
                if (image != null)
                {
                    await DownloadImageAsync(image, imageDirectory);
                    yield return image;
                }
            }
        }

        public Image LoadImageToServer(string imageName, Stream stream)
        {
            ArgumentException.ThrowIfNullOrEmpty(imageName);
            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read stream.", nameof(stream));
            }
            return _client.LoadImageAsync(imageName, stream).GetAwaiter().GetResult();
        }

        public Task<Image> LoadImageToServerAsync(string imageName, Stream stream)
        {
            ArgumentException.ThrowIfNullOrEmpty(imageName);
            if (!stream.CanRead)
            {
                throw new ArgumentException("Can not read stream.", nameof(stream));
            }
            return _client.LoadImageAsync(imageName, stream);
        }

        public Task LoadTokenToServerAsync(string tokenSource)
        {
            ArgumentException.ThrowIfNullOrEmpty(tokenSource);
            return _client.CreateTokenAsync(tokenSource);
        }

        public Task DeleteTokenFromServerAsync(string tokenSource)
        {
            ArgumentException.ThrowIfNullOrEmpty(tokenSource);
            return _client.DeleteTokenAsync(tokenSource);
        }

        public Task<List<Link>> GetAllLinksAsync()
        {
            return _client.GetLinksAsync();
        }

        public Task<List<Link>> PostLinksAsync(List<Link> links)
        {
            return _client.AddLinksAsync(links);
        }

        public Task DeleteLinkAsync(int linkId)
        {
            return _client.DeleteLinksAsync(linkId);   
        }
    }
}
