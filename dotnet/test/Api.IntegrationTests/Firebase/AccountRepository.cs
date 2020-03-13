using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Withywoods.Serialization.Json;

namespace KeepTrack.Api.IntegrationTests.Firebase
{
    /// <summary>
    /// Firebase account repository, uses Firebase API client.
    /// </summary>
    public class AccountRepository
    {
        private readonly FirebaseConfiguration _firebaseConfiguration;

        /// <summary>
        /// Create a new instance of <see cref="AccountRepository"/>.
        /// </summary>
        /// <param name="firebaseConfiguration"></param>
        public AccountRepository(FirebaseConfiguration firebaseConfiguration)
        {
            _firebaseConfiguration = firebaseConfiguration;
        }

        /// <summary>
        /// Authenticate with username/password.
        /// </summary>
        /// <returns>Received token</returns>
        public async Task<string> Authenticate()
        {
            using var httpClient = new HttpClient();

            var input = new
            {
                email = _firebaseConfiguration.Username,
                password = _firebaseConfiguration.Password,
                returnSecureToken = true
            };
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_firebaseConfiguration.ApplicationKey}";
            var response = await httpClient.PostAsync(url, new StringContent(input.ToJson(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var output = stringResponse.FromJson<VerifyPasswordResponseDto>();
            return output.IdToken;
        }
    }
}
