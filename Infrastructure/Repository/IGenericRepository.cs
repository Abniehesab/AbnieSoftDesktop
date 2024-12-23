using Domain.Entities.Common;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Infrastructure.Repository { 
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();

        Task<TEntity> GetEntityById(long entityId);

      
    }
}