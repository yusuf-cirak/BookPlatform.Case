using BookPlatform.Infrastructure.Image.Abstractions;
using BookPlatform.SharedKernel.Abstractions;
using Microsoft.AspNetCore.Http;

namespace BookPlatform.Infrastructure.Image.Concretes;

public sealed class Base64ImageService : IImageService, ISingletonService
{
    public async Task<string> UploadImageAsync(IFormFile image, string path, string fileName,
        CancellationToken cancellationToken = default)
    {
        // create base64 image from IFormFile
        using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream, cancellationToken);
        var imageBytes = memoryStream.ToArray();
        var base64String = Convert.ToBase64String(imageBytes);

        return base64String;
    }

    public Task<bool> RemoveImageAsync(string path, string fileName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}