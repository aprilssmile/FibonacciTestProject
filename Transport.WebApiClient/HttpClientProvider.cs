using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Transport.Interfaces;

namespace Transport.WebApiClient
{
    public class HttpClientProvider : IDisposable, IDataSender
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public HttpClientProvider(string uriString, ILogger logger)
        {
            _logger = logger;
            _httpClient = new HttpClient { BaseAddress = new Uri(uriString) };
        }

        public async Task SendResult<T>(T message) where T : class
        {
            try
            {
                var byteContent = GetContent(message);
                var result = await _httpClient.PostAsync("/api/fibonacci", byteContent);

                if (!result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    _logger.Error(resultContent);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private ByteArrayContent GetContent<T>(T message) where T : class
        {
            var content = JsonConvert.SerializeObject(message);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}