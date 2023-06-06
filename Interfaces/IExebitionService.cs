using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IExebitionService
    {
        Task<Exebition> CreateExebition(Exebition exebition);
        Task DeleteExebition(Guid id);
        Task<IEnumerable<Exebition>> GetAllExebitions();
        Task<Exebition> GetExebition(Guid id);
        Task UpdateExebition(Guid id, Exebition exebition);
    }
}