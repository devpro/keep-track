using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
using KeepTrack.Api.IntegrationTests.Firebase;
using Withywoods.WebTesting.Rest;

namespace KeepTrack.Api.IntegrationTests.TestingLogic.Resources
{
    public abstract class ResourceBase : ResourceTestBase
    {
        private readonly AccountRepository _accountRepository;

        private string _token;

        protected ResourceBase(string url)
            : this(new HttpClient { BaseAddress = new Uri(url) })
        {
        }

        protected ResourceBase(HttpClient httpClient)
            : base(httpClient)
        {
            _accountRepository = new AccountRepository(new FirebaseConfiguration());
            Fixture = new Fixture();
        }

        protected Fixture Fixture { get; }

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
