using AutoMapper;
using FrogExebitionAPI.DTO.ExebitionDTOs;
using FrogExebitionAPI.DTO.FrogDTOs;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FrogExebitionAPI.Services
{
    public class ExebitionService : IExebitionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExebitionService> _logger;
        private readonly IMapper _mapper;

        public ExebitionService(IUnitOfWork unitOfWork, ILogger<ExebitionService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ExebitionDtoDetail> CreateExebition(ExebitionDtoForCreate exebition)
        {

            try
            {
                var mappedExebition = _mapper.Map<Exebition>(exebition);
                var createdExebition = await _unitOfWork.Exebitions.CreateAsync(mappedExebition);
                _logger.LogInformation("Exebition Created");
                return _mapper.Map<ExebitionDtoDetail>(createdExebition);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, exebition);
                throw;
            };

        }

        public async Task<IEnumerable<ExebitionDtoDetail>> GetAllExebitions()
        {
            if (await _unitOfWork.Exebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }

            var result = await _unitOfWork.Exebitions.GetAllAsync(true);
            return _mapper.Map<IEnumerable<ExebitionDtoDetail>>(result);
        }

        public async Task<ExebitionDtoDetail> GetExebition(Guid id)
        {
            if (await _unitOfWork.Exebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var exebition = await _unitOfWork.Exebitions.GetAsync(id, true);

            if (exebition == null)
            {
                throw new NotFoundException("Entity not found");
            }

            return _mapper.Map<ExebitionDtoDetail>(exebition);
        }

        public async Task UpdateExebition(Guid id, ExebitionDtoForCreate exebition)
        {

            try
            {
                if (!await _unitOfWork.Exebitions.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                var mappedExebition = _mapper.Map<Exebition>(exebition);
                mappedExebition.Id = id;
                await _unitOfWork.Exebitions.UpdateAsync(mappedExebition);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.Exebitions.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found due to possible concurrency");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteExebition(Guid id)
        {
            if (await _unitOfWork.Exebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var exebition = await _unitOfWork.Exebitions.GetAsync(id);

            if (exebition == null)
            {
                throw new NotFoundException("Entity not found");
            }

            await _unitOfWork.Exebitions.DeleteAsync(exebition.Id);
        }

        public async Task<IEnumerable<FrogDtoRating>> GetRating(Guid id)
        {
            var exebition = await _unitOfWork.Exebitions.GetAsync(id);

            if (exebition == null)
            {
                throw new NotFoundException("Entity not found");
            }

            var frogsOnExebition = exebition.FrogsOnExebitions;
            var votes = await _unitOfWork.Votes.GetAllAsync();
            var nesVotes = votes.Join(frogsOnExebition, v => v.FrogOnExebitionId, f => f.Id, (v, f) => new { FrogOnExebition = f, Vote = v });
            var group = nesVotes.GroupBy(o => o.FrogOnExebition.FrogId).Select(o =>(new {key = o.Key, Count = o.Count()}));
            var order = group.OrderBy(o => o.Count).Reverse();
            var res = from o in order
                      let obj = _unitOfWork.Frogs.Get(o.key) // ne async????
                      select new FrogDtoRating
                      {
                          Id = obj.Id,
                          VotesCount = o.Count,
                          Color = obj.Color,
                          HouseKeepable = obj.HouseKeepable,
                          CurrentAge = obj.CurrentAge,
                          MaxAge = obj.MaxAge,
                          Photo = obj.Photo,
                          Genus = obj.Genus,
                          Habitat = obj.Habitat,
                          Weight = obj.Weight,
                          Sex = obj.Sex,
                          Poisonous = obj.Poisonous,
                          Size = obj.Size,
                          Species = obj.Species
                      };
            return res.ToList();
            //var res = order.Select(o => new { frog = _unitOfWork.Frogs.GetAsync(o.key), votesCount = o.Count });
            //var res = order.Select(async o => _mapper.Map<FrogDtoRating>(await _unitOfWork.Frogs.GetAsync(o.key)).VotesCount = o.Count).ToList();

        }
    }
}
