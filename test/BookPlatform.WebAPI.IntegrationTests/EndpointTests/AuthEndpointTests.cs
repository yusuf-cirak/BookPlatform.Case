using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BookPlatform.Application.Contracts.Auths;
using BookPlatform.Application.Features.Auths.Commands.Login;
using BookPlatform.Application.Features.Auths.Commands.Register;
using BookPlatform.WebAPI.IntegrationTests.Fixtures;
using BookPlatform.WebAPI.IntegrationTests.Priorities;
using BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;
using Xunit.Abstractions;

namespace BookPlatform.WebAPI.IntegrationTests.EndpointTests;

[Collection("BaseCollection")]
[TestCaseOrderer(PriorityConstants.FactPriorityOrdererTypeName,
    PriorityConstants.AssemblyName)]
[CollectionPriority(1)]
public class AuthEndpointTests
{
    private readonly BaseFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;

    public AuthEndpointTests(BaseFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
        _httpClient = _fixture.Factory.CreateClient();
    }

    [Fact, FactPriority(1)]
    public async Task Step1_Register_Endpoint_Should_Return_Created()
    {
        // Arrange
        var registerRequest = new RegisterCommandRequest { Username = "string2", Password = "string" };

        // Act
        var response = await _httpClient.PostAsync("/_api/auths/register",
            new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseString);
    }

    [Fact, FactPriority(2)]
    public async Task Step2_Register_Endpoint_Should_Return_BadRequest()
    {
        // Arrange
        var registerRequest = new RegisterCommandRequest { Username = "string", Password = "string" };

        // Act
        var response = await _httpClient.PostAsync("/_api/auths/register",
            new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseString);
    }

    [Fact, FactPriority(3)]
    public async Task Step3_Login_Endpoint_Should_Return_OK()
    {
        // Arrange
        var loginRequest = new LoginCommandRequest { Username = "string", Password = "string" };

        // Act
        var response = await _httpClient.PostAsync("/_api/auths/login",
            new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json"));

        // Assert

        var responseString = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.NotNull(responseString);
        
        
        // TODO: This is a workaround to set the token in the fixture. Is it a good practice?
        _fixture.Token = responseString?.Token!;
        
        Assert.NotNull(_fixture.Token);
        
    }

    [Fact, FactPriority(4)]
    public async Task Step4_Login_Endpoint_Should_Return_BadRequest()
    {
        // Arrange
        var loginRequest = new LoginCommandRequest { Username = "string4", Password = "string" };

        // Act
        var response = await _httpClient.PostAsync("/_api/auths/login",
            new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}