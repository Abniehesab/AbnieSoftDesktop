using Application.DTOS.ACC.Moein;
using Application.Services.Implementations.ACC.IKol;
using Application.Services.Implementations.ACC.MoeinTafziliGroup;
using Application.Services.Interfaces.ACC.IKol;
using Application.Services.Interfaces.ACC.IMoein;
using Application.Services.Interfaces.ACC.IMoeinTafziliGroup;
using Application.Services.Interfaces.ACC.ITafziliGroup;
using Common.Commons;
using Common.Utilities.Paging;
using Domain.Entities.ACC;

using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services.Implementations.ACC.Moein
{
    public class MoeinService : IMoeinService
    {
        #region constructor
        private IGenericRepository<Domain.Entities.ACC.Moein> MoeinRepository;
        private IGenericRepository<Domain.Entities.ACC.Kol> KolRepository;
    
        private IGenericRepository<Domain.Entities.ACC.TafziliGroup> TafziliGroupRepository;



        private IMoeinTafziliGroups _MoeinTafziliGroup;
        private ITafziliGroupService _TafziliGroupService;
        public MoeinService(IGenericRepository<Domain.Entities.ACC.Moein> moeinRepository,
            IGenericRepository<Domain.Entities.ACC.Kol> kolRepository,
      
            IMoeinTafziliGroups moeinTafziliGroups,
            ITafziliGroupService tafziliGroupService,
            IGenericRepository<Domain.Entities.ACC.TafziliGroup> tafziliGroupRepository
            )
        {
            this.MoeinRepository = moeinRepository;
            this.KolRepository = kolRepository;
            _MoeinTafziliGroup= moeinTafziliGroups;
            _TafziliGroupService= tafziliGroupService;
            this.TafziliGroupRepository = tafziliGroupRepository;
        }

        #endregion

        #region Moein Section



        public async Task<List<Domain.Entities.ACC.Moein>> GetAllMoein()
        {
            return await MoeinRepository.GetEntitiesQuery().ToListAsync();
        }



        public async Task<FilterMoeinDTO> FilterMoein(FilterMoeinDTO filter)
        {
            var moeinQuery = MoeinRepository.GetEntitiesQuery()
                .Where(moein => moein.IsDelete == false)
                .Join
                (
                    KolRepository.GetEntitiesQuery(),
                    moein => moein.KolId,
                    kol => kol.Id,
                    (moein, kol) => new { Moein = moein, Kol = kol }
                )              
                .Where(moeinQuery => moeinQuery.Moein.BusinessId == filter.BusinessId)
                .Select(moeinQuery => new MoeinDTO
                {
                    Id = moeinQuery.Moein.Id,
                    MoeinCode = moeinQuery.Moein.MoeinCode,
                    MoeinName = moeinQuery.Moein.MoeinName,
                    AccMoeinCode = moeinQuery.Moein.AccMoeinCode,
                    FirstTotalDebtorValue = moeinQuery.Moein.FirstTotalDebtorValue,
                    FirstTotalCreditorValue = moeinQuery.Moein.FirstTotalCreditorValue,
                    TotalDebtorValue = moeinQuery.Moein.TotalDebtorValue,
                    TotalCreditorValue = moeinQuery.Moein.TotalCreditorValue,
                    Finalbalance = moeinQuery.Moein.Finalbalance,
                    NatureFinalBalance = moeinQuery.Moein.NatureFinalBalance,


                    BusinessId = moeinQuery.Moein.BusinessId,
                    AccNatureId=0,
                    AccNatureName= moeinQuery.Moein.MoeinName,

                    KolId = moeinQuery.Kol.Id,
                    KolName = moeinQuery.Kol.KolName,
                    KolCode = moeinQuery.Kol.KolCode
                })
                .AsQueryable();
            switch (filter.OrderBy)
            {
                case MoeinOrderBy.CodeAsc:
                    moeinQuery = moeinQuery.OrderBy(s => s.AccMoeinCode);
                    break;
                case MoeinOrderBy.CodeDec:
                    moeinQuery = moeinQuery.OrderByDescending(s => s.AccMoeinCode);
                    break;
            }

            if (filter.Kols != null && filter.Kols.Any())
            {
                moeinQuery = moeinQuery.Where(accgroup => filter.Kols.Contains(accgroup.KolId));
            }

            if (!string.IsNullOrEmpty(filter.Title))
                moeinQuery = moeinQuery.Where(s => s.MoeinName.Contains(filter.Title));

            if (filter.AccMoeinCode > 0 && filter.AccMoeinCode != null)

                moeinQuery = moeinQuery.Where(s => s.AccMoeinCode == filter.AccMoeinCode);


            var count = (int)Math.Ceiling(moeinQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var moeins = await moeinQuery.Paging(pager).ToListAsync();




            return filter.SetMoein(moeins).SetPaging(pager);
        }

        public async Task<Domain.Entities.ACC.Moein> GetMoeins(long MoeinId)
        {
            return await MoeinRepository.GetEntitiesQuery().AsQueryable().
               SingleOrDefaultAsync(f => !f.IsDelete && f.Id == MoeinId);
        }

        public async Task<List<Domain.Entities.ACC.Moein>> GetMoeinsByIds(List<long> MoeinIds)
        {
            return await MoeinRepository.GetEntitiesQuery()
                .AsQueryable()
                .Where(f => !f.IsDelete && MoeinIds.Contains(f.Id))
                .ToListAsync();
        }
      

        public async Task<long> GetMoeinBy(int moeinCode, long BusinessId)
        {

            var moeins = await GetMoeinByBusinessId(BusinessId);
            var moein = moeins.Where(moein => moein.AccMoeinCode == moeinCode).FirstOrDefault();
            return (long)moein.Id;

        }

        public async Task<long> GetMoeinByName(string MoeinName, long BusinessId)
        {
            var moeins = await GetMoeinByBusinessId(BusinessId);
            var moein = moeins.Where(moein => moein.MoeinName == MoeinName).FirstOrDefault();
            return (long)moein.Id;
        }



        public async Task<List<Domain.Entities.ACC.Moein>> GetMoeinByBusinessId(long BusinessId)
        {
            return await MoeinRepository.GetEntitiesQuery()
              .Where(moein => moein.BusinessId == BusinessId && !moein.IsDelete)
              .ToListAsync();
        }

        public async Task<List<Domain.Entities.ACC.Moein>> GetMoeinByKolId(long KolId)
        {
            return await MoeinRepository.GetEntitiesQuery()
              .Where(moein => moein.KolId == KolId)
              .ToListAsync();
        }


        public async Task<List<Domain.Entities.ACC.Moein>> GetMoeinsByTafzili(long TafziliId)
        {
            // یافتن گروه تفضیلی بر اساس آی دی تفضیلی


            var tafziliGroup = await _TafziliGroupService.GetTafziliGroupsByTafziliId(TafziliId);

            var moeinIds = await _MoeinTafziliGroup.MoeinIdWithTafziliGroupId((long)tafziliGroup);

            return await this.GetMoeinsByIds(moeinIds);

          
        }

       

        public bool IsMoeinExistCode(int AccMoeinCode)
        {
            return MoeinRepository.GetEntitiesQuery().Any(a => a.AccMoeinCode == AccMoeinCode);
        }

        public bool IsMoeinExistName(string MoeinName)
        {
            return MoeinRepository.GetEntitiesQuery().Any(a => a.MoeinName == MoeinName);
        }

   

       
  
        #endregion

        #region Dispose
        public void Dispose()
        {
            KolRepository.Dispose();
            MoeinRepository?.Dispose();
        
        }


        #endregion
    }
}
