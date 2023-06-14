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
        private readonly ISortHelper<Frog> _sortHelper;
        private readonly IPhotoService _photoService;
        private readonly IFrogPhotoService _frogPhotoService;

        public FrogService(IUnitOfWork unitOfWork, ILogger<FrogService> logger, IMapper mapper, ISortHelper<Frog> sortHelper, IPhotoService photoService, IFrogPhotoService frogPhotoService)
        { 
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _sortHelper = sortHelper;
            _photoService = photoService;
            _frogPhotoService = frogPhotoService;
        }

        public async Task<FrogDtoDetail> CreateFrog(FrogDtoForCreate frog)
        {
            try
            {
                Frog mappedFrog = _mapper.Map<Frog>(frog);
                Frog createdFrog = await _unitOfWork.Frogs.CreateAsync(mappedFrog);
                // вызов сервисов для создания картинки в wwwroot и сохранения имени картинки в БД
                var photopath = await _photoService.SavePhotoAsync(frog.Photo); // wwwrooot save
                var frogPhoto = _frogPhotoService.CreateFrogPhotoAsync(new FrogPhoto { PhotoPath = photopath, FrogId = createdFrog.Id });
                //------
                _logger.LogInformation("Frog Created");
                var res = _mapper.Map<FrogDtoDetail>(createdFrog);
                res.PhotoPaths = (await _frogPhotoService.GetFrogPhotoPathsAsync(createdFrog.Id)).ToList();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, frog);
                throw;
            };
        }

        public async Task<FrogDtoDetail> CreateFrog(FrogDtoForCreate frog, List<IFormFile> images)
        {
            try
            {
                Frog mappedFrog = _mapper.Map<Frog>(frog);
                Frog createdFrog = await _unitOfWork.Frogs.CreateAsync(mappedFrog);
                // вызов сервисов для создания картинки в wwwroot и сохранения имени картинки в БД
                foreach (var image in images)
                {
                    var photopath = await _photoService.SavePhotoAsync(image); // wwwrooot save
                    var frogPhoto = _frogPhotoService.CreateFrogPhotoAsync(new FrogPhoto { PhotoPath = photopath, FrogId = createdFrog.Id });
                }
                //------
                _logger.LogInformation("Frog Created");
                var res = _mapper.Map<FrogDtoDetail>(createdFrog);
                res.PhotoPaths = (await _frogPhotoService.GetFrogPhotoPathsAsync(createdFrog.Id)).ToList();
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, frog);
                throw;
            };
        }
        public async Task<IEnumerable<FrogDtoGeneral>> GetAllFrogs()
        {
            if (await _unitOfWork.Frogs.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frogs = await _unitOfWork.Frogs.GetAllAsync(true);
            return _mapper.Map<IEnumerable<FrogDtoGeneral>>(frogs);
        }

        public async Task<IEnumerable<FrogDtoGeneral>> GetAllFrogs(string sortParams)
        {
            if (await _unitOfWork.Frogs.IsEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var frogs = (await _unitOfWork.Frogs.GetAllAsync(true)).AsQueryable();
            var sortedFrogs = _sortHelper.ApplySort(frogs, sortParams);
            return _mapper.Map<IEnumerable<FrogDtoGeneral>>(sortedFrogs);
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

    }
}
