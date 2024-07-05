using BookPlatform.Infrastructure.Image.Concretes;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BookPlatform.Infrastructure.UnitTests.Services;

public class Base64ImageServiceTests
{
    [Fact]
    public async Task UploadImageAsync_ShouldReturnBase64String()
    {
        // Arrange
        var mockFormFile = new Mock<IFormFile>();
        var fileName = "testImage.png";
        var path = "images/";

        // Mock IFormFile
        using var memoryStream = new MemoryStream();
        var bytes = new byte[] { 0x12, 0x34, 0x56, 0x78 };
        await memoryStream.WriteAsync(bytes);
        await memoryStream.FlushAsync();
        memoryStream.Position = 0;
        mockFormFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
        mockFormFile.Setup(f => f.FileName).Returns(fileName);

        var service = new Base64ImageService();

        // Act
        var result = await service.UploadImageAsync(mockFormFile.Object, path, fileName, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RemoveImageAsync_ShouldReturnTrue()
    {
        // Arrange
        var service = new Base64ImageService();
        var fileName = "testImage.png";
        var path = "images/";

        // Act
        var result = await service.RemoveImageAsync(path, fileName, CancellationToken.None);

        // Assert
        Assert.True(result);
    }
}