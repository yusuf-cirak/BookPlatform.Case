using Microsoft.AspNetCore.Http;

namespace BookPlatform.Infrastructure.Image.Abstractions;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile image, string path, string fileName,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveImageAsync(string path, string fileName, CancellationToken cancellationToken = default);
}