﻿using FrogExhibitionDAL.Models.Base;

namespace FrogExhibitionDAL.Interfaces
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        public IEnumerable<DbModel> GetAll(bool asNoTraking = false);
        public DbModel Get(Guid id);
        public void Create(DbModel model);
        public void CreateRange(IEnumerable<DbModel> model);
        public void Update(DbModel model);
        public void Delete(Guid id);
        public Task<IEnumerable<DbModel>> GetAllAsync(bool asNoTraking = false);
        public Task<DbModel> GetAsync(Guid id, bool asNoTraking = false);
        public Task CreateAsync(DbModel model);
        public Task CreateRangeAsync(IEnumerable<DbModel> models);
        public Task UpdateAsync(DbModel model);
        public Task DeleteAsync(Guid id);
        public Task DeleteRangeeAsync(IEnumerable<DbModel> models);
        public Task<bool> EntityExists(Guid id);
        public Task<bool> IsEmpty();


    }
}