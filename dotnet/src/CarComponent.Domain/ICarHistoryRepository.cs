using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeepTrack.CarComponent.Domain
{
    public interface ICarHistoryRepository
    {
        Task<CarHistoryModel> FindOneAsync(string id);
        Task<List<CarHistoryModel>> FindAllAsync(string carId);
    }
}
