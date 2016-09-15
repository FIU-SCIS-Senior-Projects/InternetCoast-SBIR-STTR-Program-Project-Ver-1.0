using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace InternetCoast.Infrastructure.Data.EF
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        #region ctor
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        } 
        #endregion

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _dbSet;
            return query;
        }

        //public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter = null, int take = 0, int skip = 0)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //        query = query.Where(filter);

        //    if (skip > 0)
        //        query = query.Skip(skip);

        //    if (take > 0)
        //        query = query.Take(take);

        //    return query;
        //}

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        } 

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
