using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using KeepTrack.Api.IntegrationTests.Firebase;
using Withywoods.WebTesting.Rest;

namespace KeepTrack.Api.IntegrationTests.TestingLogic.Resources
{
    public abstract class ResourceBase
    {
        private readonly AccountRepository _accountRepository;

        private string _token;

        protected ResourceBase(HttpClient httpClient, string resourceEndpoint)
        {
            _accountRepository = new AccountRepository(new FirebaseConfiguration());

            RestRunner = new RestRunner { ResourceEndpoint = resourceEndpoint };

            HttpClient = httpClient;
        }

        protected RestRunner RestRunner { get; }

        protected HttpClient HttpClient { get; }

        protected static void SetBearerToken(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task Authenticate()
        {
            _token = await _accountRepository.Authenticate();
            SetBearerToken(HttpClient, _token);
        }
    }
}
