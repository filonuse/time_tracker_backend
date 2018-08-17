using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        Task<IEnumerable<TEntity>> GetAll();
        TEntity GetById(string id);
        void Insert(IEnumerable<TEntity> entities);
        void Update(IEnumerable<TEntity> entities);
        Task Delete(IEnumerable<TEntity> entities);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
