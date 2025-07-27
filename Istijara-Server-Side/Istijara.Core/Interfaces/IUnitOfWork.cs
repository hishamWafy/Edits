using Istijara.Core.Entities;
using Istijara.Core.Interfaces.Repositories;

namespace Istijara.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> Complete();
    }
}
