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

        public async Task<Frog> CreateFrog(Frog frog)
        {
            try
            {
                frog.Id = new Guid(); //?? выглядит точно не как good practice
                var createdFrog = await _unitOfWork.Frogs.CreateAsync(frog);
                _logger.LogInformation("Frog Created");
                return createdFrog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,frog);
                throw;
            };
        }

        public async Task<IEnumerable<FrogDetailDto>> GetAllFrogs()
        {
            if (await _unitOfWork.Frogs.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frogs = _mapper.Map<IEnumerable<FrogDetailDto>>(await _unitOfWork.Frogs.GetAllAsync(true));
            return frogs;
        }

        public async Task<Frog> GetFrog(Guid id)
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

            return frog;
        }

        public async Task UpdateFrog(Guid id, Frog frog)
        {
            if (id != frog.Id)
            {
                throw new BadRequestException("Id in request parametr is differs from id in body");
            }

            try
            {
                if (!await _unitOfWork.Frogs.EntityExists(id))
                {
                    throw new NotFoundException("Entity not found");
                }
                await _unitOfWork.Frogs.UpdateAsync(frog);
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
       
    }
}
