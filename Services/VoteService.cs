using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FrogExebitionAPI.Services
{
    public class VoteService : IVoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VoteService> _logger;

        public VoteService(IUnitOfWork unitOfWork, ILogger<VoteService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Vote> CreateVote(Vote vote)
        {
            try
            {
                vote.Id = new Guid(); //?? выглядит точно не как good practice
                var createdVote = await _unitOfWork.Votes.CreateAsync(vote);
                _logger.LogInformation("Vote Created");
                return createdVote;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, vote);
                throw;
            };
        }

        public async Task<IEnumerable<Vote>> GetAllVotes()
        {
            if (await _unitOfWork.Votes.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }

            return await _unitOfWork.Votes.GetAllAsync(true);
        }

        public async Task<Vote> GetVote(Guid id)
        {
            if (await _unitOfWork.Votes.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var vote = await _unitOfWork.Votes.GetAsync(id, true);

            if (vote == null)
            {
                throw new NotFoundException("Entity not found");
            }

            return vote;
        }

        public async Task UpdateVote(Guid id, Vote vote)
        {
            if (id != vote.Id)
            {
                throw new BadRequestException("Id in request parametr is differs from id in body");
            }

            try
            {
                if (!await _unitOfWork.Votes.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                await _unitOfWork.Votes.UpdateAsync(vote);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.Votes.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found due to possible concurrency");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteVote(Guid id)
        {
            if (await _unitOfWork.Votes.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var vote = await _unitOfWork.Votes.GetAsync(id);

            if (vote == null)
            {
                throw new NotFoundException("Entity not found");
            }

            await _unitOfWork.Votes.DeleteAsync(vote.Id);
        }
    }
}
