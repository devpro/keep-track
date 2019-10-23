using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeepTrack.CarComponent.Domain
{
    public interface ICarHistoryRepository
    {
        Task<CarHistoryModel> FindOneAsync(string id);

        Task<List<CarHistoryModel>> FindAllAsync(string carId);

        Task<CarHistoryModel> CreateAsync(CarHistoryModel model);

        Task<long> UpdateAsync(string id, CarHistoryModel model);

        Task<long> DeleteAsync(string id);
    }
}
