namespace Common.Core.Services.Http
{
    public interface IHttpService
    {
        Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null);
        Task<T?> PostAsync<T>(string url, object body, Dictionary<string, string>? headers = null);
        Task<T?> PutAsync<T>(string url, object body, Dictionary<string, string>? headers = null);
        Task<bool> DeleteAsync(string url, Dictionary<string, string>? headers = null);
    }
}
