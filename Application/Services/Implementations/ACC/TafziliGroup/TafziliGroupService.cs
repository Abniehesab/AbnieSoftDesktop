
using Application.DTOS.ACC.Kol;
using Application.DTOS.ACC.Moein;
using Application.DTOS.ACC.TafziliGroup;

using Application.Services.Implementations.ACC.Moein;
using Application.Services.Implementations.ACC.MoeinTafziliGroup;
using Application.Services.Interfaces.ACC.IMoeinTafziliGroup;
using Application.Services.Interfaces.ACC.ITafzili;
using Application.Services.Interfaces.ACC.ITafziliGroup;
using Common.Utilities.Paging;
using Domain.Entities.ACC;

using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.ACC.TafziliGroup
{
    public class TafziliGroupService : ITafziliGroupService
    {
        #region constructor
        private IGenericRepository<Domain.Entities.ACC.TafziliGroup> TafziliGroupRepository;
        private IGenericRepository<Domain.Entities.ACC.Moein> MoeinRepository;
        private IGenericRepository<Domain.Entities.ACC.TafziliType> TafziliTypeRepository;
        private IGenericRepository<Domain.Entities.ACC.MoeinTafziliGroups> MoeinTafziliGroupRepository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili> TafziliRepository;

        ITafziliService _TafziliService;
        private IMoeinTafziliGroups moeinTafziliGroups;
     

        public TafziliGroupService(IGenericRepository<Domain.Entities.ACC.TafziliGroup> tafziliGroupRepository,
            IGenericRepository<Domain.Entities.ACC.Moein> moeinRepository,
            IGenericRepository<Domain.Entities.ACC.TafziliType> tafziliTypeRepository,
            IGenericRepository<Domain.Entities.ACC.MoeinTafziliGroups> MoeinTafziliGroupRepository,
            IGenericRepository<Domain.Entities.ACC.Tafzili> tafziliRepository,
            IMoeinTafziliGroups moeinTafziliGroups,
            ITafziliService tafziliService

            
           )
        {
            TafziliGroupRepository = tafziliGroupRepository;
            MoeinRepository = moeinRepository;
            this.TafziliTypeRepository = tafziliTypeRepository;
            this.moeinTafziliGroups = moeinTafziliGroups;
            this.MoeinTafziliGroupRepository = MoeinTafziliGroupRepository;
            _TafziliService = tafziliService;
            TafziliRepository = tafziliRepository;
        }



        #endregion

        #region TafziliGroup Section

      



   
    

        public async Task<FilterTafziliGroupDTO> FilterTafziliGroup(FilterTafziliGroupDTO filter)
        {


            var tafziliGroupQuery = TafziliGroupRepository.GetEntitiesQuery()
      
               .Select(grouped => new TafziliGroupDTO
               {
                   Id = grouped.Id,
                   TafziliGroupCode = grouped.TafziliGroupCode,
                   TafziliGroupName = grouped.TafziliGroupName,
                   BusinessId = grouped.BusinessId,
                   TafziliTypeId = grouped.TafziliTypeId,
           
                   TotalDebtorValue = grouped.TotalDebtorValue,
                   TotalCreditorValue = grouped.TotalCreditorValue,
                   FirstTotalDebtorValue = grouped.FirstTotalDebtorValue,
                   FirstTotalCreditorValue = grouped.FirstTotalCreditorValue,
                   Finalbalance = grouped.Finalbalance,
                   NatureFinalBalance = grouped.NatureFinalBalance,
         
               })
               .AsQueryable();


            tafziliGroupQuery.ToList();

            // ادامه کدهای مربوط به فیلتر و بازگرداندن نتیجه

            switch (filter.OrderBy)
            {
                case TafziliGroupOrderBy.CodeAsc:
                    tafziliGroupQuery = tafziliGroupQuery.OrderBy(s => s.TafziliGroupCode);
                    break;
                case TafziliGroupOrderBy.CodeDec:
                    tafziliGroupQuery = tafziliGroupQuery.OrderByDescending(s => s.TafziliGroupCode);
                    break;
            }
            if (filter.Moeins != null && filter.Moeins.Any())
            {
                var moeinIds = filter.Moeins.Select(moein => moein).ToList();

                var tafziliGroupIds = await moeinTafziliGroups.TafziliGroupIdWithMoeinId(moeinIds);
                tafziliGroupQuery = tafziliGroupQuery.Where(tafziliGroup=>tafziliGroupIds.Contains(tafziliGroup.Id));
               
            }





            if (!string.IsNullOrEmpty(filter.Title))
                tafziliGroupQuery = tafziliGroupQuery.Where(s => s.TafziliGroupName.Contains(filter.Title));

            if (filter.TafziliTypes != null && filter.TafziliTypes.Any())
            {
                tafziliGroupQuery = tafziliGroupQuery.Where(t => filter.TafziliTypes.Contains(t.TafziliTypeId));
            }



            if (filter.TafziliGroupCodeStart > 0 && filter.TafziliGroupCodeStart != null)
                tafziliGroupQuery = tafziliGroupQuery.Where(s => s.TafziliGroupCode == filter.TafziliGroupCodeStart);
           

            var count = (int)Math.Ceiling(tafziliGroupQuery.Count() / (double)filter.TakeEntity);
            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);
            var tafziliGroups =  tafziliGroupQuery.Paging(pager).ToList();

            return filter.SetTafziliGroup(tafziliGroups).SetPaging(pager);
        }

        public async Task<List<Domain.Entities.ACC.TafziliGroup>> GetTafziliGroupByBusinessId(long BusinessId)
        {
            return await TafziliGroupRepository.GetEntitiesQuery()
            .Where(tafziligroup => tafziligroup.BusinessId == BusinessId && !tafziligroup.IsDelete)
            .ToListAsync();
        }

        public async Task<Domain.Entities.ACC.TafziliGroup> GetTafziliGroups(long TafziliGroupId)
        {
            return await TafziliGroupRepository.GetEntitiesQuery().AsQueryable().
               SingleOrDefaultAsync(f => !f.IsDelete && f.Id == TafziliGroupId);
        }


        public async Task<long?> GetTafziliGroupsByTafziliId(long TafziliId)
        {
            var GetTafziliGroupByTafziliId =  await TafziliRepository.GetEntitiesQuery().AsQueryable().
               SingleOrDefaultAsync(tafzili => !tafzili.IsDelete && tafzili.Id == TafziliId);
            if (GetTafziliGroupByTafziliId != null)
            {
                return GetTafziliGroupByTafziliId.TafziliGroupId;
            }
            return null;
        }

        public async Task<List<long?>> GetTafziliGroupByTafziliId(long TafziliId)
        {
            var tafziliList = await TafziliRepository.GetEntitiesQuery().AsQueryable()
                                  .Where(t => !t.IsDelete && t.Id == TafziliId)
                                  .Select(t => t.TafziliGroupId)
                                  .ToListAsync();

            return tafziliList;
        }

        public async Task<long> GetTafziliGroupByName(string TafziliGroupName, long BusinessId)
        {
            var tafziliGroups = await GetTafziliGroupByBusinessId(BusinessId);
            var tafziliGroup = tafziliGroups.Where(tafziliGroup => tafziliGroup.TafziliGroupName == TafziliGroupName).FirstOrDefault();
            return (long)tafziliGroup.Id;
        }


   

      
        #endregion

        #region Dispose
        public void Dispose()
        {
            TafziliGroupRepository?.Dispose();
        }

      

        #endregion
    }
}
