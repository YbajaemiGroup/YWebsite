﻿using YApiModel.Models;

namespace YApi
{
    public class YApiInteractor : IApiInteractor
    {
        private readonly YClient _client;

        public YApiInteractor(IConfigInteractor configInteractor)
        {
            _client = new YClient(configInteractor.GetToken());
        }

        public List<Player> GetAllPlayers()
        {
            return _client.PlayersGetAsync().Result;
        }

        public Task<List<Player>> GetAllPlayersAsync()
        {
            return _client.PlayersGetAsync();
        }

        public Task<List<Player>> UpdatePlayersAsync(List<Player> players)
        {
            return _client.PlayersAddOrUpdateAsync(players);
        }

        public async Task DeletePlayer(int playerId)
        {
            await _client.PlayerDelete(playerId);
        }

        public Task<List<Image>> GetImagesListAsync()
        {
            return _client.GetImagesList();
        }

        public void DownloadImage(string imageName, string imagesDirectory)
        {
            using var dbImage = _client.GetImage(imageName, YApiModel.ImageType.Players).Result;
            string imagePath = $"{imagesDirectory}\\{imageName}";
            if (!File.Exists(imagePath))
            {
                using var localImage = File.OpenWrite(imagePath);
                dbImage.CopyTo(localImage);
                localImage.Flush();
                dbImage.Flush();
            }
        }

        public async Task DownloadImageAsync(string imageName, string imagesDirectory)
        {
            using var dbImage = await _client.GetImage(imageName, YApiModel.ImageType.Players);
            string imagePath = $"{imagesDirectory}\\{imageName}";
            if (!File.Exists(imagePath))
            {
                using var localImage = File.OpenWrite(imagePath);
                await dbImage.CopyToAsync(localImage);
                await localImage.FlushAsync().ConfigureAwait(false);
                await dbImage.FlushAsync().ConfigureAwait(false);
            }
        }

        public async Task DownloadImageAsync(string imageName, Stream stream)
        {
            if (!stream.CanWrite)
            {
                throw new ArgumentException("Can't write to stream.", nameof(stream));
            }
            using var dbImage = await _client.GetImage(imageName, YApiModel.ImageType.Players);
            await dbImage.CopyToAsync(stream);
        }

        public List<string> DownloadAllImages(string imageDirectory)
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
    }
}
