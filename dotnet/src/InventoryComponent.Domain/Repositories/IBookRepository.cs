using System.Collections.Generic;
using System.Threading.Tasks;
using KeepTrack.InventoryComponent.Domain.Models;

namespace KeepTrack.InventoryComponent.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<BookModel> FindOneAsync(string id, string ownerId);

        Task<List<BookModel>> FindAllAsync(string ownerId);

        Task<BookModel> CreateAsync(BookModel model);

        Task<long> UpdateAsync(string id, BookModel model, string ownerId);

        Task<long> DeleteAsync(string id, string ownerId);
    }
}
