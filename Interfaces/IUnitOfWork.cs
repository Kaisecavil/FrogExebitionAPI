using FrogExebitionAPI.Models;
using FrogExebitionAPI.Repositories;

namespace FrogExebitionAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Exebition> Exebitions { get; }
        IBaseRepository<Frog> Frogs { get; }
        IBaseRepository<FrogOnExebition> FrogOnExebitions { get; }
        IBaseRepository<User> Users { get; }
        IBaseRepository<Vote> Votes { get; }

        void Save();
    }
}