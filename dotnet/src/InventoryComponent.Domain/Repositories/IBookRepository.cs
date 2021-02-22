using KeepTrack.Domain.Repositories;
using KeepTrack.InventoryComponent.Domain.Models;

namespace KeepTrack.InventoryComponent.Domain.Repositories
{
    public interface IBookRepository : IDataRepository<BookModel>
    {
    }
}
