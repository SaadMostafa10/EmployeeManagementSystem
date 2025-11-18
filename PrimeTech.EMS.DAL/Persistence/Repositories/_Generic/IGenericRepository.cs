//using PrimeTech.EMS.DAL.Models.TEntity;
using PrimeTech.EMS.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.DAL.Persistence.Repositories._Generic
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll(bool WithAsNoTracking = true);
        IQueryable<TEntity> GetIQueryable();
        IEnumerable<TEntity> GetIEnumerable();
        TEntity? Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
