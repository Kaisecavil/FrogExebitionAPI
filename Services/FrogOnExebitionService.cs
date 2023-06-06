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

        public FrogOnExebitionService(IUnitOfWork unitOfWork, ILogger<FrogOnExebitionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<FrogOnExebition> CreateFrogOnExebition(FrogOnExebition frogOnExebition)
        {
            try
            {
                frogOnExebition.Id = new Guid(); //?? выглядит точно не как good practice
                var createdFrogOnExebition = await _unitOfWork.FrogOnExebitions.CreateAsync(frogOnExebition);
                _logger.LogInformation("FrogOnExebition Created");
                return createdFrogOnExebition;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, frogOnExebition);
                throw;
            };
        }

        public async Task<IEnumerable<FrogOnExebition>> GetAllFrogOnExebitions()
        {
            if (await _unitOfWork.FrogOnExebitions.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }

            return await _unitOfWork.FrogOnExebitions.GetAllAsync(true);
        }

        public async Task<FrogOnExebition> GetFrogOnExebition(Guid id)
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

            return frogOnExebition;
        }

        public async Task UpdateFrogOnExebition(Guid id, FrogOnExebition frogOnExebition)
        {
            if (id != frogOnExebition.Id)
            {
                throw new BadRequestException("Id in request parametr is differs from id in body");
            }

            try
            {
                if (!await _unitOfWork.FrogOnExebitions.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                await _unitOfWork.FrogOnExebitions.UpdateAsync(frogOnExebition);
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
