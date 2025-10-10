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
        IQueryable<TEntity> GetAllAsQueryable();
        TEntity? Get(int id);
        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
    }
}
