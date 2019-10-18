using System.Threading.Tasks;

namespace KeepTrack.CarComponent.Domain
{
    public interface ICarRepository
    {
        Task<CarModel> FindOneAsync(string id);
    }
}
