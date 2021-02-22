using System.Collections.Generic;
using System.Threading.Tasks;
using KeepTrack.InventoryComponent.Domain.Models;

namespace KeepTrack.InventoryComponent.Domain.Repositories
{
    public interface IDataRepository<T> where T : IDataModel
    {
        Task<T> FindOneAsync(string id, string ownerId);

        Task<List<T>> FindAllAsync(string ownerId);

        Task<T> CreateAsync(T model);

        Task<long> UpdateAsync(string id, T model, string ownerId);

        Task<long> DeleteAsync(string id, string ownerId);
    }
}
