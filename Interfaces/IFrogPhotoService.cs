using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IFrogPhotoService
    {
        Task<FrogPhoto> CreateFrogPhotoAsync(FrogPhoto frogPhoto);
        Task<IEnumerable<string>> GetFrogPhotoPathsAsync(Guid frogId);
        IEnumerable<string> GetFrogPhotoPaths(Guid frogId);
    }
}