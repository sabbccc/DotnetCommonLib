using System.IO;
using System.Threading.Tasks;

namespace Common.Core.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public LocalFileStorageService(string basePath)
        {
            _basePath = basePath;
            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task SaveFileAsync(string path, Stream content)
        {
            var fullPath = Path.Combine(_basePath, path);
            var directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await content.CopyToAsync(fs);
        }

        public Task<Stream> GetFileAsync(string path)
        {
            var fullPath = Path.Combine(_basePath, path);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {fullPath}");

            Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Task.FromResult(stream);
        }

        public Task DeleteFileAsync(string path)
        {
            var fullPath = Path.Combine(_basePath, path);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }

        public Task<bool> FileExistsAsync(string path)
        {
            var fullPath = Path.Combine(_basePath, path);
            return Task.FromResult(File.Exists(fullPath));
        }
    }
}
