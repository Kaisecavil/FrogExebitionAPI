using FrogExebitionAPI.Controllers;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Models;
using FrogExebitionAPI.UoW;
using Microsoft.EntityFrameworkCore;

namespace FrogExebitionAPI.Services
{
    public class FrogService : IFrogService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<FrogsController> _logger;

        public FrogService(UnitOfWork unitOfWork, ILogger<FrogsController> logger)
        { 
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Frog> CreateFrog(Frog frog)
        {
            //_unitOfWork.Frogs.GetAll().IsNullOrEmpty() 

            //if (await _unitOfWork.Frogs.IsEmpty())
            //{
            //    return base.Problem("Entity set 'ApplicationContext.Frogs'  is null.");
            //}


            try
            {
                var createdFrog = await _unitOfWork.Frogs.CreateAsync(frog);
                _logger.LogInformation("Frog Created");
                return createdFrog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,frog);
                throw;
            };

            //return base.CreatedAtAction("GetFrog", new { id = frog.Id }, frog);
        }

        public async Task<IEnumerable<Frog>> GetAllFrogs()
        {
            if (await _unitOfWork.Frogs.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }

            return await _unitOfWork.Frogs.GetAllAsync(true);
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
