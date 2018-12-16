using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TailendersApi.Client.Exceptions;

namespace TailendersApi.Client
{
    public class ClientBase
    {
        protected readonly IClientSettings ClientSettings;
        protected readonly ICredentialsProvider Credentials;

        public ClientBase(IClientSettings clientSettings, ICredentialsProvider credentialsProvider)
        {
            ClientSettings = clientSettings;
            Credentials = credentialsProvider;
        }

        public async Task<T> Get<T>(string url)
        {
            var response = await Send(HttpMethod.Get, url);
            var result = await GetResult<T>(response);
            return result;
        }

        public async Task<TO> Post<TI, TO>(string url, TI body)
        {
            var json = JsonConvert.SerializeObject(body);

            var response = await Send(HttpMethod.Post, url, json);
            var result = await GetResult<TO>(response);
            return result;
        }

        public async Task<TO> Put<TI, TO>(string url, TI body)
        {
            var json = JsonConvert.SerializeObject(body);

            var response = await Send(HttpMethod.Put, url, json);
            var result = await GetResult<TO>(response);
            return result;
        }

        public async Task Delete(string url)
        {
            var response = await Send(HttpMethod.Delete, url);
            await GetResult<string>(response);
        }

        public async Task<HttpResponseMessage> Send(HttpMethod method, string url, string json = "")
        {
            var client = CreateClient();
            var request = new HttpRequestMessage(method, url);

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (!string.IsNullOrWhiteSpace(json))
                {
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            var response = await client.SendAsync(request);
            return response;
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ClientSettings.BaseUrl)
            };
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Credentials.AuthenticationToken);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private async Task<T> GetResult<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ClientException((int)response.StatusCode, response.ReasonPhrase);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
    }
}
