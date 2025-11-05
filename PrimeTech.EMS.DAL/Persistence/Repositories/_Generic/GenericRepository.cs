using Microsoft.EntityFrameworkCore;
using PrimeTech.EMS.DAL.Models.Department;
using PrimeTech.EMS.DAL.Models.Shared;
using PrimeTech.EMS.DAL.Persistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistence.Repositories._Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<TEntity> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();

            return _dbContext.Set<TEntity>().ToList();
        }
        // Question not Understand
        public IQueryable<TEntity> GetIQueryable()
        {
            return _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetIEnumerable()
        {
            return _dbContext.Set<TEntity>();
        }
        public TEntity? Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
            // var TEntity = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
            // if (TEntity == null)
            //     TEntity = _dbContext.Departments.FirstOrDefault(D => D.Id == id);
            // 
            // return TEntity;
        }
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(TEntity entity)
        {
            //_dbContext.Set<TEntity>().Remove(entity);  <- Hard Delete
            entity.IsDeleted = true;                   //<- Soft Delete
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }

        
    }
}
