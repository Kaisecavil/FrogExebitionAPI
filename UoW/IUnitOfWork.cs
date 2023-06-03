using FrogExebitionAPI.Models;
using FrogExebitionAPI.Repositories.Interfaces;

namespace FrogExebitionAPI.UoW
{
    public interface IUnitOfWork
    {
        IBaseRepository<Exebition> Exebitions { get; }
        IBaseRepository<Frog> Frogs { get; }
        IBaseRepository<FrogOnExebition> FrogsOnExebitions { get; }
        IBaseRepository<User> Users { get; }
        IBaseRepository<Vote> Votes { get; }

        void Save();
    }
}