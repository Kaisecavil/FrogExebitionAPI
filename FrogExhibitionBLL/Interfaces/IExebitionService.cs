using FrogExhibitionPL.DTO.ExhibitionDTOs;
using FrogExhibitionPL.DTO.FrogDTOs;

namespace FrogExhibitionBLL.Interfaces
{
    public interface IExhibitionService
    {
        Task<ExhibitionDtoDetail> CreateExhibition(ExhibitionDtoForCreate exebition);
        Task DeleteExhibition(Guid id);
        Task<IEnumerable<ExhibitionDtoDetail>> GetAllExhibitions();
        Task<ExhibitionDtoDetail> GetExhibition(Guid id);
        Task UpdateExhibition(Guid id, ExhibitionDtoForCreate exebition);
        Task<IEnumerable<FrogDtoRating>> GetRating(Guid id);
        Task<IEnumerable<ExhibitionDtoDetail>> GetAllExhibitions(string sortParams);
    }
}