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

        public ExebitionService(IUnitOfWork unitOfWork, ILogger<ExebitionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Exebition> CreateExebition(Exebition exebition)
        {

            try
            {
                exebition.Id = new Guid();
                var createdExebition = await _unitOfWork.Exebitions.CreateAsync(exebition);
                _logger.LogInformation("Exebition Created");
                return createdExebition;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, exebition);
                throw;
            };

        }

        public async Task<IEnumerable<Exebition>> GetAllExebitions()
        {
            if (await _unitOfWork.Exebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }

            return await _unitOfWork.Exebitions.GetAllAsync(true);
        }

        public async Task<Exebition> GetExebition(Guid id)
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

            return exebition;
        }

        public async Task UpdateExebition(Guid id, Exebition exebition)
        {
            if (id != exebition.Id)
            {
                throw new BadRequestException("Id in request parametr is differs from id in body");
            }

            try
            {
                if (!await _unitOfWork.Exebitions.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                await _unitOfWork.Exebitions.UpdateAsync(exebition);
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
