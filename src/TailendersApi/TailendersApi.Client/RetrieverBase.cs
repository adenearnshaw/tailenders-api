using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TailendersApi.Client
{
    public class RetrieverBase
    {
        private readonly IClientSettings _clientSettings;
        private readonly ICredentialsProvider _credentials;

        public RetrieverBase(IClientSettings clientSettings, ICredentialsProvider credentialsProvider)
        {
            _clientSettings = clientSettings;
            _credentials = credentialsProvider;
        }

        public async Task<T> Get<T>(string url)
        {
            return await Send<T, T>(HttpMethod.Get, url);
        }

        public async Task<TO> Post<TI,TO>(string url, TI body)
        {
            return await Send<TI,TO>(HttpMethod.Post, url, body);
        }

        public async Task<TO> Put<TI,TO>(string url, TI body)
        {
            return await Send<TI,TO>(HttpMethod.Put, url, body);
        }

        public async Task Delete(string url)
        {
            await Send<string, string>(HttpMethod.Delete, url);
        }

        private async Task<TO> Send<TI,TO>(HttpMethod method, string url, TI body = default(TI))
        {
            var client = CreateClient();

            var request = new HttpRequestMessage(method, url);

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (!EqualityComparer<TI>.Default.Equals(body, default(TI)))
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                }
            }

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return default(TO);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TO>(json);

            return result;
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_clientSettings.BaseUrl)
            };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _credentials.AuthenticationToken);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
