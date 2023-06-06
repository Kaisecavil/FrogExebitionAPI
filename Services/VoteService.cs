﻿using AutoMapper;
using FrogExebitionAPI.DTO.VoteDtos;
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
        private readonly IMapper _mapper;

        public VoteService(IUnitOfWork unitOfWork, ILogger<VoteService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<VoteDtoDetail> CreateVote(VoteDtoForCreate vote)
        {
            try
            {
                var mappedVote = _mapper.Map<Vote>(vote);
                var createdVote = await _unitOfWork.Votes.CreateAsync(mappedVote);
                _logger.LogInformation("Vote Created");
                return _mapper.Map<VoteDtoDetail>(createdVote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, vote);
                throw;
            };
        }

        public async Task<IEnumerable<VoteDtoDetail>> GetAllVotes()
        {
            if (await _unitOfWork.Votes.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var result = await _unitOfWork.Votes.GetAllAsync(true);
            return _mapper.Map<IEnumerable<VoteDtoDetail>>(result);
        }

        public async Task<VoteDtoDetail> GetVote(Guid id)
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

            return _mapper.Map<VoteDtoDetail>(vote);
        }

        public async Task UpdateVote(Guid id, VoteDtoForCreate vote)
        {
            try
            {
                if (!await _unitOfWork.Votes.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                var mappedVote = _mapper.Map<Vote>(vote);
                mappedVote.Id = id;
                await _unitOfWork.Votes.UpdateAsync(mappedVote);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, vote);
                throw;
            };
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
