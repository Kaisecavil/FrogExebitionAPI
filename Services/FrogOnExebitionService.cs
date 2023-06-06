using AutoMapper;
using FrogExebitionAPI.DTO.FrogOnExebitionDTOs;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FrogExebitionAPI.Services
{
    public class FrogOnExebitionService : IFrogOnExebitionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FrogOnExebitionService> _logger;
        private readonly IMapper _mapper;

        public FrogOnExebitionService(IUnitOfWork unitOfWork, ILogger<FrogOnExebitionService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<FrogOnExebitionDtoDetail> CreateFrogOnExebition(FrogOnExebitionDtoForCreate frogOnExebition)
        {
            try
            {
                var mappedFrogOnExebition = _mapper.Map<FrogOnExebition>(frogOnExebition);
                var createdFrogOnExebition = await _unitOfWork.FrogOnExebitions.CreateAsync(mappedFrogOnExebition);
                _logger.LogInformation("FrogOnExebition Created");
                return _mapper.Map<FrogOnExebitionDtoDetail>(createdFrogOnExebition);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, frogOnExebition);
                throw;
            };
        }

        public async Task<IEnumerable<FrogOnExebitionDtoDetail>> GetAllFrogOnExebitions()
        {
            if (await _unitOfWork.FrogOnExebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var result = await _unitOfWork.FrogOnExebitions.GetAllAsync(true);
            return _mapper.Map<IEnumerable<FrogOnExebitionDtoDetail>>(result);
        }

        public async Task<FrogOnExebitionDtoDetail> GetFrogOnExebition(Guid id)
        {
            if (await _unitOfWork.FrogOnExebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frogOnExebition = await _unitOfWork.FrogOnExebitions.GetAsync(id, true);

            if (frogOnExebition == null)
            {
                throw new NotFoundException("Entity not found");
            }

            return _mapper.Map<FrogOnExebitionDtoDetail>(frogOnExebition);
        }

        public async Task UpdateFrogOnExebition(Guid id, FrogOnExebitionDtoForCreate frogOnExebition)
        {

            try
            {
                if (!await _unitOfWork.FrogOnExebitions.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                var mappedFrogOnExebition = _mapper.Map<FrogOnExebition>(frogOnExebition);
                mappedFrogOnExebition.Id = id;
                await _unitOfWork.FrogOnExebitions.UpdateAsync(mappedFrogOnExebition);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.FrogOnExebitions.EntityExists(id))
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
                _logger.LogError(ex.Message, frogOnExebition);
                throw;
            }
        }

        public async Task DeleteFrogOnExebition(Guid id)
        {
            if (await _unitOfWork.FrogOnExebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frogOnExebition = await _unitOfWork.FrogOnExebitions.GetAsync(id);

            if (frogOnExebition == null)
            {
                throw new NotFoundException("Entity not found");
            }

            await _unitOfWork.FrogOnExebitions.DeleteAsync(frogOnExebition.Id);
        }


    }
}
