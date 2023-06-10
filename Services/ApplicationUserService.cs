using AutoMapper;
using FrogExebitionAPI.DTO.ApplicatonUserDTOs;
using FrogExebitionAPI.Exceptions;
using FrogExebitionAPI.Interfaces;
using FrogExebitionAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FrogExebitionAPI.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationUserService> _logger;
        private readonly IMapper _mapper;

        public ApplicationUserService(IUnitOfWork unitOfWork, ILogger<ApplicationUserService> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        //public async Task<ApplicationUserDtoDetail> CreateApplicationUser(ApplicationUserDtoForCreate applicationUser)
        //{
        //    //try
        //    //{
        //    //    var mappedApplicationUser = _mapper.Map<ApplicationUser>(applicationUser);
        //    //    var createdApplicationUser = await _unitOfWork.ApplicationUsers.CreateAsync(mappedApplicationUser);
        //    //    _logger.LogInformation("ApplicationUser Created");
        //    //    return _mapper.Map<ApplicationUserDtoDetail>(createdApplicationUser);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    _logger.LogError(ex.Message, applicationUser);
        //    //    throw new DbUpdateException();
        //    //};
        //}

        public async Task<IEnumerable<ApplicationUserDtoDetail>> GetAllApplicationUsers()
        {
            if (_userManager.Users.IsNullOrEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var result = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationUserDtoDetail>>(result);
        }

        public async Task<ApplicationUserDtoDetail> GetApplicationUser(Guid id)
        {
            if (_userManager.Users.IsNullOrEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var applicationUser = await _userManager.Users.Where(u => u.Id == id.ToString()).FirstOrDefaultAsync();

            if (applicationUser == null)
            {
                throw new NotFoundException("Entity not found");
            }

            return _mapper.Map<ApplicationUserDtoDetail>(applicationUser);
        }

        public async Task UpdateApplicationUser(Guid id, ApplicationUserDtoForUpdate applicationUser)
        {
            try
            {
                var user = await _userManager.Users.Where(u => u.Id == id.ToString()).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new NotFoundException("Entity not found");
                }
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.UserName = applicationUser.UserName;
                user.Email = applicationUser.Email;
                user.Photo = applicationUser.Photo;
                var result = await _userManager.UpdateAsync(user);
                //var mappedApplicationUser = _mapper.Map<ApplicationUser>(applicationUser);
                //mappedApplicationUser.Id = id.ToString();
                //var result = await _userManager.UpdateAsync(mappedApplicationUser);
                //var err = result.Result.Errors.ToString();
                ////await result;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _userManager.Users.AnyAsync(u => u.Id == id.ToString()))
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
                _logger.LogError(ex.Message, applicationUser);
                throw;
            };
        }

        public async Task DeleteApplicationUser(Guid id)
        {

            if (_userManager.Users.IsNullOrEmpty())
            {
                throw new NotFoundException("Entity not found due to emptines of db");
            }
            var applicationUser = await _userManager.Users.Where(u => u.Id == id.ToString()).FirstOrDefaultAsync();

            if (applicationUser == null)
            {
                throw new NotFoundException("Entity not found");
            }

            await _userManager.DeleteAsync(applicationUser);
        }
    }
}

