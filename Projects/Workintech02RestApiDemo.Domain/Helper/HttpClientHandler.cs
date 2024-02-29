using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workintech02RestApiDemo.Domain.Helper
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly HttpClient _httpClient;

        public HttpClientHandler()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetStringAsync(string url)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", "04k2b6eIXMqOE1l1FLM4fWrVOqMmdc7e");
            return await _httpClient.GetStringAsync(url);
        }
    }
}
