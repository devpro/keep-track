using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KeepTrack.Api.IntegrationTests.TestingLogic.Resources
{
    public class GenericResource<T> : ResourceBase
    {
        public GenericResource(HttpClient httpClient, string resourceEndpoint)
            : base(httpClient, resourceEndpoint)
        {
        }

        public async Task<List<T>> FindAll()
        {
            return await RestRunner.GetResources<T>(HttpClient);
        }

        public async Task<T> FindOneById(string id, T expected)
        {
            return await RestRunner.GetResourceById(id, HttpClient, expected);
        }

        public async Task<T> Create()
        {
            return await RestRunner.CreateResource<T>(HttpClient);
        }

        public async Task Update(string id, T input)
        {
            await RestRunner.UpdateResource(id, input, HttpClient);
        }

        public async Task Delete(string id)
        {
            await RestRunner.DeleteResource(id, HttpClient);
        }
    }
}
