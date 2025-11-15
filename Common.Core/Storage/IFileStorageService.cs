using System.IO;
using System.Threading.Tasks;

namespace Common.Core.Storage
{
    public interface IFileStorageService
    {
        Task SaveFileAsync(string path, Stream content);
        Task<Stream> GetFileAsync(string path);
        Task DeleteFileAsync(string path);
        Task<bool> FileExistsAsync(string path);
    }
}
