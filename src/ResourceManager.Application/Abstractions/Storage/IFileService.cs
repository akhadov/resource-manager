

using Microsoft.AspNetCore.Http;

namespace ResourceManager.Application.Abstractions.Storage;

public interface IFileService
{
    public Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(string fileId);
}
