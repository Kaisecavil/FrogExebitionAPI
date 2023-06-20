﻿using FrogExhibitionBLL.DTO.ApplicatonUserDTOs;

namespace FrogExhibitionBLL.Interfaces
{
    public interface IApplicationUserService
    {
        Task DeleteApplicationUser(Guid id);
        Task<IEnumerable<ApplicationUserDtoDetail>> GetAllApplicationUsers();
        Task<ApplicationUserDtoDetail> GetApplicationUser(Guid id);
        Task UpdateApplicationUser(Guid id, ApplicationUserDtoForUpdate applicationUser);
    }
}