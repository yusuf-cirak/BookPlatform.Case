using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BookPlatform.Application.Features.UserFriends.Commands.Create;
using BookPlatform.SharedKernel.Extensions;
using BookPlatform.WebAPI.IntegrationTests.Fixtures;
using BookPlatform.WebAPI.IntegrationTests.Priorities;
using BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;
using Xunit.Abstractions;

namespace BookPlatform.WebAPI.IntegrationTests.EndpointTests;

[Collection("BaseCollection")]
[TestCaseOrderer(PriorityConstants.FactPriorityOrdererTypeName,
    PriorityConstants.AssemblyName)]
[CollectionPriority(2)]
public class UserFriendEndpointTests
{
    private readonly BaseFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;

    public UserFriendEndpointTests(BaseFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
        _httpClient = _fixture.Factory.CreateClient();
    }


    [Fact, FactPriority(1)]
    public async Task Step1_AddFriend_Endpoint_Should_Return_OK()
    {
        // Arrange

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.Token);

        var addFriendRequest =
            new CreateUserFriendCommandRequest(GuidExtensions.GenerateGuidFromString("string1").ToString());


        // Act

        var response = await _httpClient.PostAsync("/_api/user-friends",
            new StringContent(JsonSerializer.Serialize(addFriendRequest), Encoding.UTF8, "application/json"));


        // Assert
        response.EnsureSuccessStatusCode();
    }

    
    // TODO: Can't test this. EF Core is not translating ExecuteDeleteAsync with in-memory database.
    
    // [Fact, FactPriority(2)]
    // public async Task Step2_RemoveFriend_Endpoint_Should_Return_Deleted()
    // {
    //     // Arrange
    //
    //     _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _fixture.Token);
    //
    //     var userId = GuidExtensions.GenerateGuidFromString("string1").ToString();
    //
    //     // Act
    //
    //     var response = await _httpClient.DeleteAsync($"/_api/user-friends/{userId}");
    //
    //
    //     var responseString = await response.Content.ReadAsStringAsync();
    //
    //     _testOutputHelper.WriteLine(responseString);
    //
    //     // Assert
    //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //
    //     Assert.NotNull(responseString);
    // }
}