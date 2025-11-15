using Polly;
using Polly.Retry;
using System.Net.Http.Json;

namespace Common.Core.Services.Http
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ResiliencePipeline _pipeline;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            // Build a retry pipeline: 3 attempts, exponential backoff
            _pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(1)
                })
                .Build();
        }

        public async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
        {
            var client = _httpClientFactory.CreateClient();
            AddHeaders(client, headers);

            return await _pipeline.ExecuteAsync(async (ct) =>
            {
                return await client.GetFromJsonAsync<T>(url, cancellationToken: ct);
            });
        }

        public async Task<T?> PostAsync<T>(string url, object body, Dictionary<string, string>? headers = null)
        {
            var client = _httpClientFactory.CreateClient();
            AddHeaders(client, headers);

            return await _pipeline.ExecuteAsync(async (ct) =>
            {
                var response = await client.PostAsJsonAsync(url, body, ct);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>(cancellationToken: ct);
            });
        }

        public async Task<T?> PutAsync<T>(string url, object body, Dictionary<string, string>? headers = null)
        {
            var client = _httpClientFactory.CreateClient();
            AddHeaders(client, headers);

            return await _pipeline.ExecuteAsync(async (ct) =>
            {
                var response = await client.PutAsJsonAsync(url, body, ct);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>(cancellationToken: ct);
            });
        }

        public async Task<bool> DeleteAsync(string url, Dictionary<string, string>? headers = null)
        {
            var client = _httpClientFactory.CreateClient();
            AddHeaders(client, headers);

            return await _pipeline.ExecuteAsync(async (ct) =>
            {
                var response = await client.DeleteAsync(url, ct);
                return response.IsSuccessStatusCode;
            });
        }

        private void AddHeaders(HttpClient client, Dictionary<string, string>? headers)
        {
            if (headers == null) return;

            foreach (var kvp in headers)
            {
                client.DefaultRequestHeaders.Remove(kvp.Key);
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
