using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IFrogOnExebitionService
    {
        Task<FrogOnExebition> CreateFrogOnExebition(FrogOnExebition frogOnExebition);
        Task DeleteFrogOnExebition(Guid id);
        Task<IEnumerable<FrogOnExebition>> GetAllFrogOnExebitions();
        Task<FrogOnExebition> GetFrogOnExebition(Guid id);
        Task UpdateFrogOnExebition(Guid id, FrogOnExebition frogOnExebition);
    }
}