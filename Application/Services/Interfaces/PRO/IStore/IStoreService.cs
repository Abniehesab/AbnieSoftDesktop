using Application.DTOS.PRO.Store;
using Domain.Entities.PRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.IStore
{
    public interface IStoreService
    {
        #region Store      
        Task<FilterStoreDTO> FilterStore(FilterStoreDTO filter);
        Task<Store> GetStores(long StoreId);     
        Task<List<Store>> GetStoreByBusinessId(long BusinessId);
        #endregion
    }
}
