using System;
using System.Net.Http;

namespace TestXamarin.Services
{
    public class TrelloService
    {
        readonly HttpClient _client;

        public TrelloService()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(TrelloApi.BaseUrl)
            };
            //_client.DefaultRequestHeaders.Add("Accept", $"{BinarySerializer.MEDIA_TYPE}; charset={BinarySerializer.DefaultEncoding.WebName}");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        protected HttpClient Client => _client;
    }
}
