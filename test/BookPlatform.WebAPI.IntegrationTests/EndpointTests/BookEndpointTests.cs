using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using BookPlatform.Application.Features.Books.Commands.Create;
using BookPlatform.Application.Features.Books.Commands.Update;
using BookPlatform.Application.Features.Books.Dtos;
using BookPlatform.Domain.ValueObjects;
using BookPlatform.SharedKernel.Extensions;
using BookPlatform.WebAPI.IntegrationTests.Fixtures;
using BookPlatform.WebAPI.IntegrationTests.Priorities;
using BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;
using Xunit.Abstractions;

namespace BookPlatform.WebAPI.IntegrationTests.EndpointTests;

[Collection("BaseCollection")]
[TestCaseOrderer(PriorityConstants.FactPriorityOrdererTypeName, PriorityConstants.AssemblyName)]
[CollectionPriority(3)]
public class BookEndpointTests
{
    private readonly BaseFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;

    public BookEndpointTests(BaseFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
        _httpClient = _fixture.Factory.CreateClient();
    }

    [Fact, FactPriority(1)]
    public async Task Step1_CreateBook_Should_Return_Created()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.Token);

        var shelfLocation = ShelfLocation.Create("Room1", "SectionA", "Shelf3", "Position5");

        var createBookCommandRequest = new CreateBookCommandRequest
        {
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            Isbn = "978-0743273565",
            Pages = 180,
            Genre = "Classic",
            PublishedDate = new DateTime(1925, 4, 10),
            Description = "A novel set in the Jazz Age on Long Island.",
            ShelfLocation = shelfLocation
            // Picture property is omitted intentionally
        };

        // Create key-value pairs for form data
        var formData = new Dictionary<string, string>
        {
            { "Title", createBookCommandRequest.Title },
            { "Author", createBookCommandRequest.Author },
            { "Isbn", createBookCommandRequest.Isbn },
            { "Pages", createBookCommandRequest.Pages.ToString() },
            { "Genre", createBookCommandRequest.Genre },
            { "PublishedDate", createBookCommandRequest.PublishedDate.ToString("o") },
            { "Description", createBookCommandRequest.Description },
            { "ShelfLocation.Room", createBookCommandRequest.ShelfLocation.Room },
            { "ShelfLocation.Section", createBookCommandRequest.ShelfLocation.Section },
            { "ShelfLocation.Shelf", createBookCommandRequest.ShelfLocation.Shelf },
            { "ShelfLocation.Position", createBookCommandRequest.ShelfLocation.Position }
        };

        // Convert to FormUrlEncodedContent
        var content = new FormUrlEncodedContent(formData);

        // Act
        var response = await _httpClient.PostAsync("/_api/books", content);

        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var bookDto = JsonSerializer.Deserialize<GetBookDto>(responseString);
        Assert.NotNull(bookDto);
    }


    // TODO: Can't test this. EF Core is not mapping ShelfLocation complex type with in-memory database.
    // [Fact, FactPriority(2)]
    // public async Task Step2_GetBookById_Should_Return_OK()
    // {
    //     // Arrange
    //     var bookId = GuidExtensions.GenerateGuidFromString("deneme").ToString();
    //
    //     // Act
    //     var response = await _httpClient.GetAsync($"/_api/books/{bookId}");
    //
    //     // Assert
    //     var responseString = await response.Content.ReadAsStringAsync();
    //     _testOutputHelper.WriteLine($"Response: {responseString}");
    //
    //     response.EnsureSuccessStatusCode();
    //
    //     Assert.NotNull(responseString);
    // }
    //
    // [Fact, FactPriority(3)]
    // public async Task Step3_GetBooksByFilter_Should_Return_OK()
    // {
    //     // Arrange
    //     var filter = "Classic";
    //
    //     // Act
    //     var response = await _httpClient.GetAsync($"/_api/books?filter={filter}");
    //
    //     // Assert
    //     var responseString = await response.Content.ReadAsStringAsync();
    //     _testOutputHelper.WriteLine($"Response: {responseString}");
    //
    //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //
    //     Assert.NotNull(responseString);
    // }

    [Fact, FactPriority(4)]
    public async Task Step4_UpdateBook_Should_Return_OK()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.Token);

        var shelfLocation = ShelfLocation.Create("Room1", "SectionA", "Shelf3", "Position5");

        var updateBookCommandRequest = new UpdateBookCommandRequest()
        {
            Id = GuidExtensions.GenerateGuidFromString("deneme").ToString(),
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            Isbn = "978-0743273565",
            Pages = 180,
            Genre = "Classic",
            PublishedDate = new DateTime(1925, 4, 10),
            Description = "A novel set in the Jazz Age on Long Island.",
            ShelfLocation = shelfLocation
            // Picture property is omitted intentionally
        };

        // Create key-value pairs for form data
        var formData = new Dictionary<string, string>
        {
            { "Id", updateBookCommandRequest.Id },
            { "Title", updateBookCommandRequest.Title },
            { "Author", updateBookCommandRequest.Author },
            { "Isbn", updateBookCommandRequest.Isbn },
            { "Pages", updateBookCommandRequest.Pages.ToString() },
            { "Genre", updateBookCommandRequest.Genre },
            { "PublishedDate", updateBookCommandRequest.PublishedDate.ToString("o") },
            { "Description", updateBookCommandRequest.Description },
            { "ShelfLocation.Room", updateBookCommandRequest.ShelfLocation.Room },
            { "ShelfLocation.Section", updateBookCommandRequest.ShelfLocation.Section },
            { "ShelfLocation.Shelf", updateBookCommandRequest.ShelfLocation.Shelf },
            { "ShelfLocation.Position", updateBookCommandRequest.ShelfLocation.Position }
        };

        // Convert to FormUrlEncodedContent
        var content = new FormUrlEncodedContent(formData);


        // Act
        var response = await _httpClient.PutAsync("/_api/books", content);

        var responseString = await response.Content.ReadAsStringAsync();

        _testOutputHelper.WriteLine($"Response: {responseString}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // TODO: Can't test this. EF Core is not translating ExecuteDeleteAsync with in-memory database.
    
    // [Fact, FactPriority(5)]
    // public async Task Step5_DeleteBook_Should_Return_NoContent()
    // {
    //     // Arrange
    //     var bookId = GuidExtensions.GenerateGuidFromString("deneme").ToString();
    //
    //     // Act
    //     var response = await _httpClient.DeleteAsync($"/_api/books/{bookId}");
    //
    //     var responseString = await response.Content.ReadAsStringAsync();
    //
    //     _testOutputHelper.WriteLine($"Response: {responseString}");
    //
    //
    //     // Assert
    //     Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    // }
}