//using AutoMapper;
//using FrogExebitionAPI.DTO.ApplicationUserDtos;
//using FrogExebitionAPI.Exceptions;
//using FrogExebitionAPI.Interfaces;
//using FrogExebitionAPI.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace FrogExebitionAPI.Services
//{
//    public class ApplicationUserService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly ILogger<ApplicationUserService> _logger;
//        private readonly IMapper _mapper;

//        public ApplicationUserService(IUnitOfWork unitOfWork, ILogger<ApplicationUserService> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
//        {
//            _unitOfWork = unitOfWork;
//            _logger = logger;
//            _mapper = mapper;
//            _userManager = userManager;
//        }

//        public async Task<ApplicationUserDtoDetail> CreateApplicationUser(ApplicationUserDtoForCreate applicationUser)
//        {
//            //try
//            //{
//            //    var mappedApplicationUser = _mapper.Map<ApplicationUser>(applicationUser);
//            //    var createdApplicationUser = await _unitOfWork.ApplicationUsers.CreateAsync(mappedApplicationUser);
//            //    _logger.LogInformation("ApplicationUser Created");
//            //    return _mapper.Map<ApplicationUserDtoDetail>(createdApplicationUser);
//            //}
//            //catch (Exception ex)
//            //{
//            //    _logger.LogError(ex.Message, applicationUser);
//            //    throw new DbUpdateException();
//            //};
//        }

//        public async Task<IEnumerable<ApplicationUserDtoDetail>> GetAllApplicationUsers()
//        {
//            //if (await _unitOfWork.ApplicationUsers.IsEmpty())
//            //{
//            //    throw new NotFoundException("Entity not found due to emptines of db");
//            //}
//            //var result = await _unitOfWork.ApplicationUsers.GetAllAsync(true);
//            //return _mapper.Map<IEnumerable<ApplicationUserDtoDetail>>(result);
//        }

//        public async Task<ApplicationUserDtoDetail> GetApplicationUser(Guid id)
//        {
//            //if (await _unitOfWork.ApplicationUsers.IsEmpty())
//            //{
//            //    throw new NotFoundException("Entity not found due to emptines of db");
//            //}
//            //var applicationUser = await _unitOfWork.ApplicationUsers.GetAsync(id, true);

//            //if (applicationUser == null)
//            //{
//            //    throw new NotFoundException("Entity not found");
//            //}

//            //return _mapper.Map<ApplicationUserDtoDetail>(applicationUser);
//        }

//        public async Task UpdateApplicationUser(Guid id, ApplicationUserDtoForCreate applicationUser)
//        {
//            //try
//            //{
//            //    if (!await _unitOfWork.ApplicationUsers.EntityExists(id))
//            //    {
//            //        throw new NotFoundException("Entity not found");
//            //    }
//            //    var userApplicationUsersOnExebitionCount = _unitOfWork.ApplicationUsers.GetAll().Where(v => v.ApplicationUserId == applicationUser.ApplicationUserId && v.FrogOnExebitionId == applicationUser.FrogOnExebitionId).Count();
//            //    if (userApplicationUsersOnExebitionCount >= 3)
//            //    {
//            //        throw new DbUpdateException("This user has cast all of his available applicationUsers on this exebiton");
//            //    }
//            //    var mappedApplicationUser = _mapper.Map<ApplicationUser>(applicationUser);
//            //    mappedApplicationUser.Id = id;
//            //    await _unitOfWork.ApplicationUsers.UpdateAsync(mappedApplicationUser);
//            //}
//            //catch (DbUpdateConcurrencyException)
//            //{
//            //    if (!await _unitOfWork.ApplicationUsers.EntityExists(id))
//            //    {
//            //        throw new NotFoundException("Entity not found due to possible concurrency");
//            //    }
//            //    else
//            //    {
//            //        throw;
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    _logger.LogError(ex.Message, applicationUser);
//            //    throw;
//            //};
//        }

//        public async Task DeleteApplicationUser(Guid id)
//        {
//            //await _userManager.DeleteAsync(_userManager.GetUserAsync(ClaimsPrincipal))
//            //if (await _unitOfWork.ApplicationUsers.IsEmpty())
//            //{
//            //    throw new NotFoundException("Entity not found due to emptines of db");
//            //}
//            //var applicationUser = await _unitOfWork.ApplicationUsers.GetAsync(id);

//            //if (applicationUser == null)
//            //{
//            //    throw new NotFoundException("Entity not found");
//            //}

//            //await _unitOfWork.ApplicationUsers.DeleteAsync(applicationUser.Id);
//        }
//    }
//}
//}
