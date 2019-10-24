using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeepTrack.CarComponent.Domain
{
    public interface ICarHistoryRepository
    {
        Task<CarHistoryModel> FindOneAsync(string id, string ownerId);

        Task<List<CarHistoryModel>> FindAllAsync(string carId, string ownerId);

        Task<CarHistoryModel> CreateAsync(CarHistoryModel model);

        Task<long> UpdateAsync(string id, CarHistoryModel model, string ownerId);

        Task<long> DeleteAsync(string id, string ownerId);
    }
}
