using AutoMapper;
using FrogExebitionAPI.DTO.ExebitionDTOs;
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
    }
}
