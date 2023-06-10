using FrogExebitionAPI.DTO.ApplicatonUserDTOs;

namespace FrogExebitionAPI.Interfaces
{
    public interface IApplicationUserService
    {
        Task DeleteApplicationUser(Guid id);
        Task<IEnumerable<ApplicationUserDtoDetail>> GetAllApplicationUsers();
        Task<ApplicationUserDtoDetail> GetApplicationUser(Guid id);
        Task UpdateApplicationUser(Guid id, ApplicationUserDtoForUpdate applicationUser);
    }
}