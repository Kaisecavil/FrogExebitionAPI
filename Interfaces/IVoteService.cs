using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IVoteService
    {
        Task<Vote> CreateVote(Vote vote);
        Task DeleteVote(Guid id);
        Task<IEnumerable<Vote>> GetAllVotes();
        Task<Vote> GetVote(Guid id);
        Task UpdateVote(Guid id, Vote vote);
    }
}