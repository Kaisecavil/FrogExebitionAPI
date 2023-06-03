using FrogExebitionAPI.Models.Base;

namespace FrogExebitionAPI.Repositories.Interfaces
{
    public interface IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        public List<TDbModel> GetAll();
        public TDbModel Get(Guid id);
        public TDbModel Create(TDbModel model);
        public TDbModel Update(TDbModel model);
        public void Delete(Guid id);
        public Task<List<TDbModel>> GetAllAsync(bool asNoTraking = false);
        public Task<TDbModel> GetAsync(Guid id, bool asNoTraking = false);
        public Task<TDbModel> CreateAsync(TDbModel model);
        public Task<TDbModel> UpdateAsync(TDbModel model);
        public Task DeleteAsync(Guid id);
        public Task<bool> EntityExists(Guid id);
        public Task<bool> IsEmpty();


    }
}
