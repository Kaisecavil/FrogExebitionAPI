using FrogExebitionAPI.DTO.FrogOnExebitionDTOs;
using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IFrogOnExebitionService
    {
        Task<FrogOnExebitionDtoDetail> CreateFrogOnExebition(FrogOnExebitionDtoForCreate frogOnExebition);
        Task DeleteFrogOnExebition(Guid id);
        Task<IEnumerable<FrogOnExebitionDtoDetail>> GetAllFrogOnExebitions();
        Task<FrogOnExebitionDtoDetail> GetFrogOnExebition(Guid id);
        Task UpdateFrogOnExebition(Guid id, FrogOnExebitionDtoForCreate frogOnExebition);
    }
}