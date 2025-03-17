using NHibernate;
using StudentManagement.NHibernate.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.NHibernate.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ISession _session;

        public GenericRepository(ISession session)
        {
            _session = session;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    await _session.SaveAsync(entity);
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<T> CreateAndReturnAsync(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    await _session.SaveAsync(entity);
                    await transaction.CommitAsync();
                    return entity;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _session.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => _session.Query<T>().ToList());
        }

        public IQueryable<T> Queryable()
        {
            return _session.Query<T>();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    await _session.UpdateAsync(entity);
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var entity = await _session.GetAsync<T>(id);
                    if (entity == null) return false;

                    await _session.DeleteAsync(entity);
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> DeleteRangeAsync(List<T> entities)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        await _session.DeleteAsync(entity);
                    }
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> CreateRangeAsync(List<T> entities)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        await _session.SaveAsync(entity);
                    }
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
        public async Task<IQueryable<T>> QueryAbleAsync()
        {
            return await Task.Run(() => _session.Query<T>());
        }
    }
}
