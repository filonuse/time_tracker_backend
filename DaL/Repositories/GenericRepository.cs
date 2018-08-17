using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DaL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private DbSet<TEntity> _entities;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<TEntity>();
                }
                return _entities;
            }
        }

        public IQueryable<TEntity> Table
        {
            get { return Entities; }
        }

        public async Task<IEnumerable<TEntity>> GetAll() => Entities;

        public TEntity GetById(string id) => Entities.Find(id);

        public void Insert(IEnumerable<TEntity> entities)
        {
            try
            {
                entities.ToList().ForEach(entity => Entities.Add(entity));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            try
            {
                entities.ToList().ForEach(entity => _context.Entry(entity).State = EntityState.Modified);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task Delete(IEnumerable<TEntity> entities)
        {
            try
            {
                entities.ToList().ForEach(entity => _context.Entry(entity).State = EntityState.Deleted);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Insert(TEntity entity)
        {
            Insert(new List<TEntity>() { entity });
        }

        public void Update(TEntity entity)
        {
            Update(new List<TEntity>() { entity });
        }

        public async Task Delete(TEntity entity)
        {
            await Delete(new List<TEntity>() { entity });
        }
    }
}
