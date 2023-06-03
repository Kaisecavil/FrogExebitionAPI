using FrogExebitionAPI.Database;
using FrogExebitionAPI.Models;
using FrogExebitionAPI.Repositories.Implementations;
using FrogExebitionAPI.Repositories.Interfaces;

namespace FrogExebitionAPI.UoW
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ApplicationContext _db;
        private IBaseRepository<User> _userRepository;
        private IBaseRepository<Frog> _frogRepository;
        private IBaseRepository<Exebition> _exebitionRepository;
        private IBaseRepository<FrogOnExebition> _frogOnExebitionRepository;
        private IBaseRepository<Vote> _voteRepository;

        public UnitOfWork(ApplicationContext db)
        {
            _db = db;
        }

        public IBaseRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new BaseRepository<User>(_db);
                return _userRepository;
            }
        }
        public IBaseRepository<Frog> Frogs
        {
            get
            {
                if (_frogRepository == null)
                    _frogRepository = new BaseRepository<Frog>(_db);
                return _frogRepository;
            }
        }
        public IBaseRepository<Exebition> Exebitions
        {
            get
            {
                if (_exebitionRepository == null)
                    _exebitionRepository = new BaseRepository<Exebition>(_db);
                return _exebitionRepository;
            }
        }
        public IBaseRepository<FrogOnExebition> FrogsOnExebitions
        {
            get
            {
                if (_frogOnExebitionRepository == null)
                    _frogOnExebitionRepository = new BaseRepository<FrogOnExebition>(_db);
                return _frogOnExebitionRepository;
            }
        }
        public IBaseRepository<Vote> Votes
        {
            get
            {
                if (_voteRepository == null)
                    _voteRepository = new BaseRepository<Vote>(_db);
                return _voteRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
