using System;
using System.Data.Entity;
using System.Linq;
using Employee.Domain;

namespace Employee.Infrastructure.Repositories
{
    class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>().Where(x => x.DeletedOn == null);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entityToDelete);
            }
            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public TEntity Find(int? id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            _context.Set<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
