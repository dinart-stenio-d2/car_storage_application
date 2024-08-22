using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace car_storage_application.API.Controllers.V1.Controllers
{
    public class AuthController : CarStorageBaseController
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var clientId = _configuration["AzureAdB2C:ClientId"];
            var authority = _configuration["AzureAdB2C:Authority"];
            var redirectUri = _configuration["AzureAdB2C:RedirectUri"];
            var scopes = _configuration["AzureAdB2C:Scopes"].Split(' ');

            var app = PublicClientApplicationBuilder.Create(clientId)
                .WithB2CAuthority(authority)
                .WithRedirectUri(redirectUri)
                .Build();

            var result = await app.AcquireTokenInteractive(scopes)
                .ExecuteAsync();

            string accessToken = result.AccessToken;

            return Ok(new { AccessToken = accessToken });
        }
    }
}
