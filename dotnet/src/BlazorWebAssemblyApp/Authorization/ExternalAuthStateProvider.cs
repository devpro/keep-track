using System;
using System.Security.Claims;
using System.Threading.Tasks;
using KeepTrack.BlazorWebAssemblyApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace KeepTrack.BlazorWebAssemblyApp.Authorization
{
    public class ExternalAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILogger<ExternalAuthStateProvider> _logger;

        private UserModel _userModel;

        public ExternalAuthStateProvider(ILogger<ExternalAuthStateProvider> logger)
            => _logger = logger;

        public void UpdateAuthentitationState(string displayName, string emailAddress, string token)
        {
            _userModel = new UserModel { DisplayName = displayName, EmailAddress = emailAddress, Token = token };
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (string.IsNullOrEmpty(_userModel?.DisplayName))
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, _userModel.DisplayName),
                    new Claim(ClaimTypes.Email, _userModel.EmailAddress),
                    new Claim("Token", _userModel.Token)
                },
                "Federated authentication");

            _logger.LogInformation($"{nameof(GetAuthenticationStateAsync)} called at {DateTime.Now.ToLongTimeString()}");

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
