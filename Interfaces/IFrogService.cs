using FrogExebitionAPI.DTO.FrogDTOs;
using FrogExebitionAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrogExebitionAPI.Interfaces
{
    public interface IFrogService
    {
        public Task<IEnumerable<FrogDto>> GetAllFrogs();
        public Task<Frog> GetFrog(Guid id);
        public Task<Frog> CreateFrog(Frog frog);
        public Task DeleteFrog(Guid id);
        public Task UpdateFrog(Guid id, Frog frog);
    }
}
