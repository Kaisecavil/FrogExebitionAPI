using Microsoft.AspNetCore.Http;

namespace FrogExhibitionBLL.Interfaces
{
    public interface IPhotoService
    {
        Task<string> SavePhotoAsync(IFormFile file);
    }
}