using System.Security.Claims;
using BookPlatform.Application.Contracts.JWT;
using BookPlatform.Domain;
using BookPlatform.Infrastructure.Helpers.JWT;
using BookPlatform.Infrastructure.Helpers.Security.Encryption;
using Microsoft.Extensions.Configuration;

namespace BookPlatform.Infrastructure.UnitTests.Helpers;

 public class JwtHelperTests
    {
        private readonly IConfigurationRoot _configuration;

        public JwtHelperTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "TokenOptions:Issuer", "testIssuer" },
                    { "TokenOptions:Audience", "testAudience" },
                    { "TokenOptions:AccessTokenExpiration", "30" },
                    { "TokenOptions:SecurityKey", "this_is_a_security_key_for_my_case_application" }
                }!)
                .Build();

            JwtHelper.Configuration = _configuration;
            JwtHelper.TokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            // Initialize _accessTokenExpiration
            JwtHelper._accessTokenExpiration = DateTime.UtcNow.AddMinutes(JwtHelper.TokenOptions.AccessTokenExpiration);;
        }

        [Fact]
        public void CreateAccessToken_ShouldReturnAccessToken()
        {
            // Arrange
            var user = new User { Id = "testUserId" };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testName")
            };

            // Act
            var accessToken = JwtHelper.CreateAccessToken(user, claims);

            // Assert
            Assert.NotNull(accessToken);
            Assert.NotEmpty(accessToken.Token);
            Assert.True(accessToken.Expiration > DateTime.UtcNow);
        }

        [Fact]
        public void CreateJwtSecurityToken_ShouldReturnJwtSecurityToken()
        {
            // Arrange
            var user = new User { Id = "testUserId" };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testName")
            };
            var securityKey = SecurityKeyHelper.CreateSecurityKey("testSecurityKey");
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            // Act
            var jwt = JwtHelper.CreateJwtSecurityToken(JwtHelper.TokenOptions, user, signingCredentials, claims);

            // Assert
            Assert.NotNull(jwt);
            Assert.Equal("testIssuer", jwt.Issuer);
            Assert.Equal("testAudience", jwt.Audiences.First());
            Assert.True(jwt.ValidTo > DateTime.UtcNow);
        }

        [Fact]
        public void SetClaims_ShouldReturnCorrectClaims()
        {
            // Arrange
            var user = new User { Id = "testUserId" };
            var additionalClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testName")
            };

            // Act
            var claims = JwtHelper.SetClaims(user, additionalClaims);

            // Assert
            var claimList = claims.ToList();
            Assert.Contains(claimList, c => c.Type == ClaimTypes.NameIdentifier && c.Value == "testUserId");
            Assert.Contains(claimList, c => c.Type == ClaimTypes.Name && c.Value == "testName");
        }
    }