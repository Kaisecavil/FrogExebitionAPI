using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IFrogPhotoService
    {
        Task<FrogPhoto> CreateFrogPhotoAsync(FrogPhoto frogPhoto);
        IEnumerable<string> GetFrogPhotoPaths(Guid frogId);
        Task<IEnumerable<string>> GetFrogPhotoPathsAsync(Guid frogId);
        Task DeleteFrogPhotosAsync(Guid frogId);
    }
}