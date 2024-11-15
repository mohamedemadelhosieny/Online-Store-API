using Store.Omda.Core.Entities;
using Store.Omda.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

        IGenericRepository<TEntity , TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
