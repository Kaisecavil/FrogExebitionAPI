using FrogExebitionAPI.DTO.VoteDtos;
using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Interfaces
{
    public interface IVoteService
    {
        Task<VoteDtoDetail> CreateVote(VoteDtoForCreate vote);
        Task DeleteVote(Guid id);
        Task<IEnumerable<VoteDtoDetail>> GetAllVotes();
        Task<VoteDtoDetail> GetVote(Guid id);
        Task UpdateVote(Guid id, VoteDtoForCreate vote);
    }
}