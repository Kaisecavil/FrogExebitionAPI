using AutoMapper;
using FrogExebitionAPI.Controllers;
using FrogExebitionAPI.DTO.FrogDTOs;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FrogExebitionAPI.Services
{
    public class FrogService : IFrogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FrogService> _logger;
        private readonly IMapper _mapper;

        public FrogService(IUnitOfWork unitOfWork, ILogger<FrogService> logger, IMapper mapper)
        { 
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<FrogDtoDetail> CreateFrog(FrogDtoForCreate frog)
        {
            try
            {
                Frog mappedFrog = _mapper.Map<Frog>(frog);
                Frog createdFrog = await _unitOfWork.Frogs.CreateAsync(mappedFrog);
                _logger.LogInformation("Frog Created");
                return _mapper.Map<FrogDtoDetail>(createdFrog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,frog);
                throw;
            };
        }

        public async Task<IEnumerable<FrogDtoGeneral>> GetAllFrogs()
        {
            if (await _unitOfWork.Frogs.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frogs = _mapper.Map<IEnumerable<FrogDtoGeneral>>(await _unitOfWork.Frogs.GetAllAsync(true));
            return frogs;
        }

        public async Task<FrogDtoDetail> GetFrog(Guid id)
        {
            if (await _unitOfWork.Frogs.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frog = await _unitOfWork.Frogs.GetAsync(id, true);

            if (frog == null)
            {
                throw new NotFoundException("Entity not found");
            }

            return _mapper.Map<FrogDtoDetail>(frog);
        }

        public async Task UpdateFrog(Guid id, FrogDtoForUpdate frog)
        {
            try
            {
                if (!await _unitOfWork.Frogs.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                Frog mappedFrog = _mapper.Map<Frog>(frog);
                mappedFrog.Id = id;
                await _unitOfWork.Frogs.UpdateAsync(mappedFrog);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.Frogs.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found due to possible concurrency");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteFrog(Guid id)
        {
            if (await _unitOfWork.Frogs.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frog = await _unitOfWork.Frogs.GetAsync(id);

            if (frog == null)
            {
                throw new NotFoundException("Entity not found");
            }

            await _unitOfWork.Frogs.DeleteAsync(frog.Id);
        }

        //public async Task<IEnumerable<FrogDtoGeneral>> GetFrogsRating()
        //{
        //    if (await _unitOfWork.Frogs.IsEmpty())
        //    {
        //        throw new NotFoundException("Entity not found due to emptines of db");
        //    }
        //    //var frogs = _mapper.Map<IEnumerable<FrogDtoGeneral>>(await _unitOfWork.Frogs.GetAllAsync(true));
        //    var votes = await _unitOfWork.Votes.GetAllAsync(true);
        //    var fOnE = await _unitOfWork.FrogOnExebitions.GetAllAsync(true);
        //    var frogs = await _unitOfWork.Frogs.GetAllAsync(true);
        //    votes.First().FrogOnExebition.
        //    //var res = votes.Join(fOnE,v => v.FrogOnExebitionId,f => f.Id, (v,f) => new { })
        //    return frogs;
        //}

    }
}
