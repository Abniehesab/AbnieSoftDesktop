
using Application.DTOS.PRO.Store;
using Application.Services.Interfaces.PRO.IStore;
using Microsoft.EntityFrameworkCore;
using Common.Utilities.Paging;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace Application.Services.Implementations.PRO.Store
{
    public class StoreService : IStoreService
    {
        #region constructor
        private IGenericRepository<Domain.Entities.PRO.Store> _StoreRepository;      
        private IGenericRepository<Domain.Entities.ACC.Moein> _MoeinRepository;
      

        public StoreService(IGenericRepository<Domain.Entities.PRO.Store> StoreRepository, IGenericRepository<Domain.Entities.ACC.Moein> moeinRepository)
        {
            _StoreRepository = StoreRepository;          
            _MoeinRepository = moeinRepository;
         
        }

        #endregion   

        public async Task<List<Domain.Entities.PRO.Store>> GetStoreByBusinessId(long BusinessId)
        {
            return await EFCore.ToListAsync(_StoreRepository.GetEntitiesQuery()
             .Where(contract => contract.BusinessId == BusinessId && !contract.IsDelete));
        }

        public async Task<FilterStoreDTO> FilterStore(FilterStoreDTO filter)
        {


            var contractQuery =
                (from Store in _StoreRepository.GetEntitiesQuery()

            
                 select new StoreDTO
                 {
                                  
                     Id = Store.Id,              
                    
                     StoreTitle = Store.StoreTitle,
                                      
                 }).AsQueryable();
          

            switch (filter.OrderBy)
            {
                case StoreOrderBy.CodeAsc:
                    contractQuery = contractQuery.OrderBy(s => s.StoreCode);
                    break;
                case StoreOrderBy.CodeDec:
                    contractQuery = contractQuery.OrderByDescending(s => s.StoreCode);
                    break;
            }
            if (!string.IsNullOrEmpty(filter.Title))
                contractQuery = contractQuery.Where(s => s.StoreTitle.Contains(filter.Title));

            if (filter.StoreCode > 0 && filter.StoreCode != null)

                contractQuery = contractQuery.Where(s => s.StoreCode == filter.StoreCode);

         


            var count = (int)Math.Ceiling(contractQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var contract = contractQuery.Paging(pager).ToList();

            return filter.SetStore(contract).SetPaging(pager);

        }



        public async Task<Domain.Entities.PRO.Store> GetStores(long StoreId)
        {
            return await _StoreRepository.GetEntitiesQuery().AsQueryable().
             SingleOrDefaultAsync(contract => !contract.IsDelete && contract.Id == StoreId);
        }
 


    }
}
