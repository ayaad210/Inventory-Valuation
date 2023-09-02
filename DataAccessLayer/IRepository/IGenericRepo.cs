using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {



        public Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        public Task<TEntity> GetByID(object id);


        public Task<TEntity> Insert(TEntity entity);



        public Task<bool> Delete(object id);


        public Task<bool> Delete(TEntity entityToDelete);


        public Task<TEntity> Update(TEntity entityToUpdate);

    }
}