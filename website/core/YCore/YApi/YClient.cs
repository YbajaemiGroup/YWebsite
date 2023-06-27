using System.Text.Json;
using YApiModel;
using YApiModel.Models;

namespace YApi;

public class YClient
{
    private const string URL = "";

    public string Token { get; set; }
    public bool TokenLoaded { get => Token != null; }
    private RequestSender requestSender;

    public YClient(string token)
    {
        Token = token;
        requestSender = new(URL);
    }

    public async Task<List<Round>> GetBracketAsync()
    {
        var response = await requestSender.SendRequestAsync("bracket.updates.get");
        if (response.Exception != null)
            throw response.Exception;
        if (response.ResponseData == null)
            throw new NullReferenceException();
        return JsonSerializer.Deserialize<List<Round>>((JsonDocument)response.ResponseData) ?? throw new NullReferenceException();
    }

    public async Task SetBracketAsync(List<Round> bracket)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("bracket.updates.set", parameters, new(bracket));
        if (response.Exception != null)
            throw response.Exception;
    }

    public async Task GroupFillAsync(List<GroupFillData> groups)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("group.fill", parameters, new(groups));
        if (response.Exception != null)
            throw response.Exception;
    }

    public async Task GroupGetGames()
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("group.games.get", parameters);
        if (response.Exception != null)
            throw response.Exception;
    }

    public async Task<List<Link>> GetLinksAsync(int playerId)
    {
        var parameters = new HttpParameters();
        parameters.Add("player_id", playerId);
        var response = await requestSender.SendRequestAsync("links.get", parameters);
        if (response.Exception != null)
            throw response.Exception;
        if (response.ResponseData == null)
            throw new NullReferenceException();
        return JsonSerializer.Deserialize<List<Link>>((JsonDocument)response.ResponseData) ?? throw new NullReferenceException();
    }

    public async Task<List<Link>> GetLinksAsync()
    {
        var response = await requestSender.SendRequestAsync("links.get");
        if (response.Exception != null)
            throw response.Exception;
        if (response.ResponseData == null)
            throw new NullReferenceException();
        return JsonSerializer.Deserialize<List<Link>>((JsonDocument)response.ResponseData) ?? throw new NullReferenceException();
    }

    public async Task<List<Link>> AddLinksAsync(List<Link> links)
    {
        var parameters = new HttpParameters();
        parameters.Add("token", Token);
        var response = await requestSender.SendRequestAsync("link.add", parameters, new(links));
        if (response.Exception != null)
            throw response.Exception;
        if (response.ResponseData == null)
            throw new NullReferenceException();
        return JsonSerializer.Deserialize<List<Link>>((JsonDocument)response.ResponseData) ?? throw new NullReferenceException();
    }
}