using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Serialization;
using YApiModel;
using YApiModel.Models;

namespace YApi;

public class YClient
{
    private const string URL = "http://localhost:12345/api/";

    public string Token { get; set; }
    public bool TokenLoaded { get => Token != null; }
    private readonly RequestSender requestSender;

    public YClient(string token)
    {
        Token = token;
        requestSender = new(URL);
    }

    /// <summary>
    /// Throws exception if server returned error.
    /// </summary>
    /// <param name="response">Server response object</param>
    /// <exception cref="Exception"></exception>
    private void CheckException(Response response)
    {
        if (response.Exception != null)
        {
            throw new Exception(response.Exception.ToString());
        }
    }

    /// <summary>
    /// Gets data from server response and casting it to <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Specified response data type</typeparam>
    /// <param name="response"></param>
    /// <returns>Response data</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="ArgumentException"></exception>
    private static T GetResponseData<T>(Response response)
    {
        if (response.ResponseData == null)
        {
            throw new NullReferenceException("ResponseData was null.");
        }
        if (response.ResponseData is JsonDocument document)
        {
            var data = JsonSerializer.Deserialize<T>(document);
            if (data == null)
            {
                throw new NullReferenceException("Deserialized object was null.");
            }
            return data;
        }
        else if (response.ResponseData is JsonElement element)
        {
            var data = JsonSerializer.Deserialize<T>(element);
            if (data == null)
            {
                throw new NullReferenceException("Deserialized object was null.");
            }
            return data;
        }
        throw new ArgumentException("Response data wasn't JsonDocument or JsonElement.", nameof(response));
    }

    public async Task<List<Round>> GetBracketAsync()
    {
        var bracketGetting = requestSender.SendRequestAsync("bracket.updates.get");
        var response = await bracketGetting;
        if (!bracketGetting.IsCompletedSuccessfully)
        {
            throw bracketGetting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<List<Round>>(response);
    }

    public async Task SetBracketAsync(List<Round> bracket)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var bracketSetting = requestSender.SendRequestAsync("bracket.updates.set", parameters, new Request(bracket));
        var response = await bracketSetting;
        if (!bracketSetting.IsCompletedSuccessfully)
        {
            throw bracketSetting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
    }

    public async Task GroupFillAsync(List<GroupFillData> groups)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var groupFilling = requestSender.SendRequestAsync("group.fill", parameters, new Request(groups));
        var response = await groupFilling;
        if (!groupFilling.IsCompletedSuccessfully)
        {
            throw groupFilling.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
    }

    public async Task<List<GroupGetData>> GroupGetGamesAsync()
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var groupGamesGetting = requestSender.SendRequestAsync("group.get", parameters);
        var response = await groupGamesGetting;
        if (!groupGamesGetting.IsCompletedSuccessfully)
        {
            throw groupGamesGetting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<List<GroupGetData>>(response);
    }

    public async Task<List<Link>> GetLinksAsync(int playerId)
    {
        var parameters = new HttpParameters();
        parameters.Add("player_id", playerId);
        var linksGetting = requestSender.SendRequestAsync("links.get", parameters);
        var response = await linksGetting;
        if (!linksGetting.IsCompletedSuccessfully)
        {
            throw linksGetting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<List<Link>>(response);
    }

    public async Task<List<Link>> GetLinksAsync()
    {
        var linksGetting = requestSender.SendRequestAsync("links.get");
        var response = await linksGetting;
        if (!linksGetting.IsCompletedSuccessfully)
        {
            throw linksGetting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<List<Link>>(response);
    }

    public async Task<List<Link>> AddLinksAsync(List<Link> links)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var linksAdding = requestSender.SendRequestAsync("links.add", parameters, new Request(links));
        var response = await linksAdding;
        if (!linksAdding.IsCompletedSuccessfully)
        {
            throw linksAdding.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<List<Link>>(response);
    }

    public async Task DeleteLinksAsync(int linkId)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("link_id", linkId);
        var linksDeleting = requestSender.SendRequestAsync("links.delete", parameters);
        var response = await linksDeleting;
        if (!linksDeleting.IsCompletedSuccessfully)
        {
            throw linksDeleting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
    }

    /// <summary>
    /// Add or update players in database. Adds player if id is null. Overwise, update existing one.
    /// </summary>
    /// <param name="players">List of players need to add or update.</param>
    /// <returns>List updated of players from database.</returns>
    public async Task<List<Player>> PlayersAddOrUpdateAsync(List<Player> players)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var playersAdding = requestSender.SendRequestAsync("players.add", parameters, new Request(players));
        var response = await playersAdding;
        if (!playersAdding.IsCompletedSuccessfully)
        {
            throw playersAdding.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<List<Player>>(response);
    }

    public async Task<List<Player>> PlayersGetAsync()
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var playersGetting = requestSender.SendRequestAsync("players.get", parameters);
        var response = await playersGetting;
        if (!playersGetting.IsCompletedSuccessfully)
        {
            throw playersGetting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<List<Player>>(response);
    }

    public async Task<Player> PlayerDelete(int playerId)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("player_id", playerId);
        var playersDeleting = requestSender.SendRequestAsync("players.delete", parameters);
        var response = await playersDeleting;
        if (!playersDeleting.IsCompletedSuccessfully)
        {
            throw playersDeleting.Exception ?? throw new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<Player>(response);
    }

    public async Task<Stream> GetImage(string imageName, ImageType imageType)
    {
        string imageTypeName = imageType switch
        {
            ImageType.Resources => "resources",
            ImageType.Players => "players",
            _ => throw new ArgumentOutOfRangeException(nameof(imageType))
        };
        var parameters = new HttpParameters();
        parameters.Add("image_name", imageName);
        parameters.Add("image_type", imageTypeName);
        var requestSending = requestSender.DownloadImageAsync("images.get", parameters);
        var stream = await requestSending;
        if (!requestSending.IsCompletedSuccessfully)
        {
            throw requestSending.Exception ?? new Exception("requestSender.DownloadImageAsync returned unsuccessfully with no exception.");
        }
        return stream;
    }

    public async Task<Image> LoadImageAsync(string imageName, byte[] imageBytes)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("image_name", imageName);
        var fileStreamContent = new StreamContent(new MemoryStream(imageBytes));
        fileStreamContent.Headers.ContentType = new(imageName.Split('.')[1] switch
        {
            "jpeg" => "image/jpeg",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            _ => throw new ArgumentException("Image should be .png .jpg of .jpeg", nameof(imageName))
        });
        var imageLoading = requestSender.SendRequestAsync("images.load", parameters, fileStreamContent);
        var response = await imageLoading;
        if (!imageLoading.IsCompletedSuccessfully)
        {
            throw imageLoading.Exception ?? new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<Image>(response);
    }

    public async Task<Image> LoadImageAsync(string imageName, Stream stream)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("image_name", imageName);
        var fileStreamContent = new StreamContent(stream);
        fileStreamContent.Headers.ContentType = new(imageName.Split('.')[1] switch
        {
            "jpeg" => "image/jpeg",
            "jpg" => "image/jpeg",
            "png" => "image/png",
            _ => throw new ArgumentException("Image should be .png .jpg of .jpeg", nameof(imageName))
        });
        var imageLoading = requestSender.SendRequestAsync("images.load", parameters, fileStreamContent);
        var response = await imageLoading;
        if (!imageLoading.IsCompletedSuccessfully)
        {
            throw imageLoading.Exception ?? new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
        return GetResponseData<Image>(response);
    }

    public async Task CreateTokenAsync(string tokenSource)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("token_source", tokenSource);
        var tokenCreating = requestSender.SendRequestAsync("token.create", parameters);
        var response = await tokenCreating;
        if (!tokenCreating.IsCompletedSuccessfully)
        {
            throw tokenCreating.Exception ?? new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
    }

    public async Task DeleteTokenAsync(string token)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        parameters.Add("d_token", token);
        var tokenDeleteting = requestSender.SendRequestAsync("token.delete", parameters);
        var response = await tokenDeleteting;
        if (!tokenDeleteting.IsCompletedSuccessfully)
        {
            throw tokenDeleteting.Exception ?? new Exception("requestSender.SendRequestAsync returned unsuccessfully with no exception.");
        }
        CheckException(response);
    }
}