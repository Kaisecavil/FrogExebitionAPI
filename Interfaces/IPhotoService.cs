namespace FrogExebitionAPI.Interfaces
{
    public interface IPhotoService
    {
        Task<string> SavePhotoAsync(IFormFile file);
    }
}