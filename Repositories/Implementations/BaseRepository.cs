using FrogExebitionAPI.Database;
using FrogExebitionAPI.Models;
using FrogExebitionAPI.Models.Base;
using FrogExebitionAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FrogExebitionAPI.Repositories.Implementations
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        protected readonly ApplicationContext _context;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public TDbModel Create(TDbModel model)
        {
            _context.Set<TDbModel>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(Guid id)
        {
            var toDelete = _context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            _context.Set<TDbModel>().Remove(toDelete);
            _context.SaveChanges();
        }

        public List<TDbModel> GetAll()
        {
            return _context.Set<TDbModel>().ToList();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpdate = _context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }
            _context.Update(toUpdate);
            _context.SaveChanges();
            return toUpdate;
        }

        public TDbModel Get(Guid id)
        {
            return _context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }

        public async Task<List<TDbModel>> GetAllAsync(bool asNoTraking = false)
        {
            if (!asNoTraking)
            {
                return await _context.Set<TDbModel>().ToListAsync();
            }
            else
            {
                return await _context.Set<TDbModel>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<TDbModel> GetAsync(Guid id, bool asNoTraking = false)
        {
            if (!asNoTraking)
            {
                TDbModel model = await _context.Set<TDbModel>().FirstOrDefaultAsync(m => m.Id == id);
                return model;
            }
            else
            {
                TDbModel model = await _context.Set<TDbModel>().AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                return model;
            }
        }

        public async Task<TDbModel> CreateAsync(TDbModel model)
        {
            _context.Set<TDbModel>().Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<TDbModel> UpdateAsync(TDbModel model)
        {
            //_context.Entry(model).State = EntityState.Modified;
            var toUpdate = await GetAsync(model.Id,true);
            if (toUpdate != null)
            {
                toUpdate = model;
                _context.Update(toUpdate);
                await _context.SaveChangesAsync();
                return toUpdate;
            }
            return null;
            
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await GetAsync(id);
            _context.Set<TDbModel>().Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EntityExists(Guid id)
        {
            return await _context.Frogs.AsNoTracking().AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsEmpty()
        {
            //return _context.Frogs.AsNoTracking().IsNullOrEmpty();
            return !await _context.Frogs.AsNoTracking().AnyAsync();
        }
    }
}
