using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BookPlatform.Application.Contracts.Auths;
using BookPlatform.Application.Features.Auths.Commands.Login;
using BookPlatform.Application.Features.BookNotes.Commands.Create;
using BookPlatform.Application.Features.BookNotes.Dtos;
using BookPlatform.Domain.Enums;
using BookPlatform.SharedKernel.Extensions;
using BookPlatform.WebAPI.IntegrationTests.Fixtures;
using BookPlatform.WebAPI.IntegrationTests.Priorities;
using BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;
using Xunit.Abstractions;

namespace BookPlatform.WebAPI.IntegrationTests.EndpointTests;

[Collection("BookNoteCollection")]
[TestCaseOrderer(PriorityConstants.FactPriorityOrdererTypeName,
    PriorityConstants.AssemblyName)]
[CollectionPriority(4)]
public class BookNoteEndpointTests
{
    private readonly BookNoteFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;

    public BookNoteEndpointTests(BookNoteFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
        _httpClient = _fixture.Factory.CreateClient();
    }

    [Fact, FactPriority(1)]
    public async Task Step1_Login_Should_Return_OK()
    {
        // Arrange
        var loginReq = new LoginCommandRequest
        {
            Username = "string",
            Password = "string"
        };

        // Act
        var response = await _httpClient.PostAsync("/_api/auths/login",
            new StringContent(JsonSerializer.Serialize(loginReq), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var authDto = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

        Assert.NotNull(authDto);

        _fixture.Token = authDto.Token;
    }

    [Fact, FactPriority(2)]
    public async Task Step2_Create_Public_BookNote_Should_Return_Created()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", _fixture.Token);

        var createRequest = new CreateBookNoteCommandRequest(GuidExtensions.GenerateGuidFromString("deneme").ToString(),
            "Public book note", ShareType.Public);

        // Act
        var response = await _httpClient.PostAsync("/_api/book-notes",
            new StringContent(JsonSerializer.Serialize(createRequest), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<GetBookNoteDto>();

        Assert.NotNull(content);

        _fixture.BookNoteId = content.BookNoteId;
    }

    [Fact, FactPriority(3)]
    public async Task Step3_Get_PublicBookNoteById_ForOwner_Should_Return_OK()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", _fixture.Token);
        var id = _fixture.BookNoteId;

        // Act
        var response = await _httpClient.GetAsync($"/_api/book-notes/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        // You can add more assertions based on your expected response structure
    }

    [Fact, FactPriority(4)]
    public async Task Step4_Create_Private_BookNote_Should_Return_OK()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", _fixture.Token);
        
        var createReq =
            new CreateBookNoteCommandRequest(GuidExtensions.GenerateGuidFromString("deneme").ToString(),
                "Private book note text", ShareType.Private);

        // Act
        var response = await _httpClient.PostAsync("/_api/book-notes",
            new StringContent(JsonSerializer.Serialize(createReq), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<GetBookNoteDto>();
        Assert.NotNull(content);
        
        _fixture.BookNoteId = content.BookNoteId;
    }

    [Fact, FactPriority(5)]
    public async Task Step5_Get_PrivateBookNote_ForOwner_Should_Return_OK()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", _fixture.Token);
        var id = _fixture.BookNoteId;

        // Act
        var response = await _httpClient.GetAsync($"/_api/book-notes/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }

    [Fact, FactPriority(6)]
    public async Task Step6_Get_PrivateBookNote_ForAnonymous_Should_Return_Unauthorized()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = null;
        
        var id = _fixture.BookNoteId;

        // Act
        var response = await _httpClient.GetAsync($"/_api/book-notes/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact, FactPriority(7)]
    public async Task Step7_Create_OnlyFriends_BookNote_Should_Return_OK()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", _fixture.Token);
        
        var createReq =
            new CreateBookNoteCommandRequest(GuidExtensions.GenerateGuidFromString("deneme").ToString(),
                "Only friends book note text", ShareType.OnlyFriends);

        // Act
        var response = await _httpClient.PostAsync("/_api/book-notes",
            new StringContent(JsonSerializer.Serialize(createReq), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<GetBookNoteDto>();
        Assert.NotNull(content);
        
        _fixture.BookNoteId = content.BookNoteId;
    }

    [Fact, FactPriority(8)]
    public async Task Step8_Get_OnlyFriends_BookNote_ForOwner_Should_Return_OK()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.Token);
        var id = _fixture.BookNoteId;

        // Act
        var response = await _httpClient.GetAsync($"/_api/book-notes/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }

    [Fact, FactPriority(9)]
    public async Task Step9_Login_AsFriend_Should_Return_OK()
    {
        // Arrange
        var loginReq = new LoginCommandRequest
        {
            Username = "string1",
            Password = "string"
        };

        // Act
        var response = await _httpClient.PostAsync("/_api/auths/login",
            new StringContent(JsonSerializer.Serialize(loginReq), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var authDto = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

        Assert.NotNull(authDto);

        _fixture.Token = authDto.Token;
    }


    [Fact, FactPriority(10)]
    public async Task Step10_Get_OnlyFriends_BookNote_ForFriend_Should_Return_OK()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.Token);
        var id = _fixture.BookNoteId;

        var response = await _httpClient.GetAsync($"/_api/book-notes/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }

    [Fact, FactPriority(11)]
    public async Task Step11_Get_OnlyFriends_BookNote_ForAnonymous_Should_Return_Unauthorized()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = null;

        var id = _fixture.BookNoteId;

        // Act
        var response = await _httpClient.GetAsync($"/_api/book-notes/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
    }

    // TODO: Can't test this. EF Core is not translating ExecuteDeleteAsync method with in-memory database.
    // [Fact, FactPriority(12)]
    // public async Task Step12_DeleteBookNote_Should_Return_NoContent()
    // {
    //     // Arrange
    //     _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", _fixture.Token);
    //     var id = _fixture.BookNoteId;
    //
    //     // Act
    //     var response = await _httpClient.DeleteAsync($"/_api/book-notes/{id}");
    //
    //     // Assert
    //     Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    // }
}