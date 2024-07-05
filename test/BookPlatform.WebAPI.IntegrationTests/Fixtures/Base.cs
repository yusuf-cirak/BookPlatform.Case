namespace BookPlatform.WebAPI.IntegrationTests.Fixtures;

[CollectionDefinition("BaseCollection")]
public sealed class BaseCollection : ICollectionFixture<BaseFixture>;

public class BaseFixture
{
    public CustomWebApplicationFactory<Program> Factory { get; } = new();
    public string Token { get; set; }
}