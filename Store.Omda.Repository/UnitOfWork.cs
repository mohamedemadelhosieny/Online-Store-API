using Store.Omda.Core;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Repositories.Contract;
using Store.Omda.Repository.Data.Contexts;
using Store.Omda.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        public Hashtable _Repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _Repositories = new Hashtable();

        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;

            if (!_Repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity, TKey>(_context);

                _Repositories.Add(type, repository);
            }

            return _Repositories[type] as IGenericRepository<TEntity, TKey>;
        }
    }
}
