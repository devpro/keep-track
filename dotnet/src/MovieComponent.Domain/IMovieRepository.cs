using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeepTrack.MovieComponent.Domain
{
    public interface IMovieRepository
    {
        Task<MovieModel> FindOneAsync(string id, string ownerId);

        Task<List<MovieModel>> FindAllAsync(string ownerId);

        Task<MovieModel> CreateAsync(MovieModel model);

        Task<long> UpdateAsync(string id, MovieModel model, string ownerId);

        Task<long> DeleteAsync(string id, string ownerId);
    }
}
