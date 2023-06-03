using FrogExebitionAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrogExebitionAPI.Services
{
    public interface IFrogService
    {
        public Task<IEnumerable<Frog>> GetAllFrogs();
        public Task<Frog> GetFrog(Guid id);
        public Task<Frog> CreateFrog(Frog frog);
        public Task DeleteFrog(Guid id);
        public Task UpdateFrog(Guid id,Frog frog);
    }
}
