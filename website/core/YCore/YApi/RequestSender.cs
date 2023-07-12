using System.Net.Http.Json;
using System.Text.Json;
using YApiModel;

namespace YApi
{
    internal class RequestSender
    {
        private readonly string _url;
        private readonly HttpClient httpClient;

        public RequestSender(string url)
        {
            _url = url.EndsWith('/') ? url : url + "/";
            httpClient = new HttpClient();
        }

        public string GetUrlWithParameters(string method, HttpParameters httpParameters) => $"{_url}{method}?{httpParameters}";

        public async Task<Stream> DownloadImageAsync(string method, HttpParameters parameters)
        {
            using var message = new HttpRequestMessage();
            message.RequestUri = new(parameters == null ? _url : GetUrlWithParameters(method, parameters));
            var responseMessage = httpClient.Send(message);
            Response response = null!;
            try
            {
                var r = await responseMessage.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<Response>(r) ?? throw new ArgumentNullException();
                throw new Exception(response.Exception?.ToString());
            }
            catch (Exception e) when (e is ArgumentNullException or JsonException or NotSupportedException)
            {
                var imageBytes = await responseMessage.Content.ReadAsStreamAsync();
                return imageBytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> SendRequestAsync(string method, HttpParameters? parameters = null, Request? request = null)
        {
            using var message = new HttpRequestMessage();
            message.RequestUri = new(parameters == null ? _url + method : GetUrlWithParameters(method, parameters));
            if (request != null)
            {
                message.Content = JsonContent.Create(request);
            }
            var responseMessage = httpClient.Send(message);
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<Response>(responseString);
            if (response == null)
            {
                throw new NullReferenceException("Server somehow returned null.");
            }
            return response;
        }

        public async Task<Response> SendRequestAsync(string method, HttpParameters? parameters, HttpContent content)
        {
            using var message = new HttpRequestMessage();
            message.RequestUri = new(parameters == null ? _url : GetUrlWithParameters(method, parameters));
            message.Content = content;
            var responseMessage = await httpClient.SendAsync(message);
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<Response>(responseString);
            if (response == null)
            {
                throw new NullReferenceException("Server somehow returned null.");
            }
            return response;
        }

        public async Task<Response> SendRequestAsync(string method, HttpParameters? parameters, byte[] body)
        {
            using var message = new HttpRequestMessage();
            message.RequestUri = new(parameters == null ? _url : GetUrlWithParameters(method, parameters));
            message.Content = new ByteArrayContent(body);
            var responseMessage = await httpClient.SendAsync(message);
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<Response>(responseString);
            if (response == null)
            {
                throw new NullReferenceException("Server somehow returned null.");
            }
            return response;
        }
    }
}
