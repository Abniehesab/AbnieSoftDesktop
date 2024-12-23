using Application.DTOS.ACC.Kol;
using Application.Services.Interfaces.ACC.IKol;
using Application.Services.Interfaces.ACC.IKol;
using Application.Services.Interfaces.ACC.IMoein;
using Common.Utilities.Paging;
using Domain.Entities.ACC;

using Infrastructure.Repository;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.ACC.IKol
{
    public class KolService : IKolService
    {
        #region constructor
        private IGenericRepository<Domain.Entities.ACC.Kol> KolRepository;
     
        private IGenericRepository<Domain.Entities.ACC.Moein> moeinRepository;
        private IMoeinService moeinService;
        

        public KolService(IGenericRepository<Domain.Entities.ACC.Kol> kolRepository, IMoeinService moeinService, IGenericRepository<Domain.Entities.ACC.Moein> moeinRepository)
        {
            this.KolRepository = kolRepository;
       
            this.moeinService = moeinService;
            this.moeinRepository = moeinRepository;
        }

        #endregion

        #region Kol Section
  
        public async Task<FilterKolDTO> FilterKol(FilterKolDTO filter)
        {
            var kolQuery = KolRepository.GetEntitiesQuery()
               
                .Select(kolQuery => new KolDTO
                {
                    Id = kolQuery.Id,
                    KolCode = kolQuery.KolCode,
                    KolName = kolQuery.KolName,
                    AccKolCode = kolQuery.AccKolCode,
                    FirstTotalDebtorValue = kolQuery.FirstTotalDebtorValue,
                    FirstTotalCreditorValue = kolQuery.FirstTotalCreditorValue,
                    TotalDebtorValue = kolQuery.TotalDebtorValue,
                    TotalCreditorValue = kolQuery.TotalCreditorValue,
                    Finalbalance = kolQuery.Finalbalance,
                    NatureFinalBalance = kolQuery.NatureFinalBalance,

                    BusinessId = kolQuery.BusinessId,

                    AccGroupId = 0,
                    AccGroupName = kolQuery.KolName,
                    AccGroupCode = kolQuery.KolCode
                })
                .AsQueryable();
            switch (filter.OrderBy)
            {
                case KolOrderBy.CodeAsc:
                    kolQuery = kolQuery.OrderBy(s => s.AccKolCode);
                    break;
                case KolOrderBy.CodeDec:
                    kolQuery = kolQuery.OrderByDescending(s => s.AccKolCode);
                    break;
            }

            if (filter.AccGroups != null && filter.AccGroups.Any())
            {
                kolQuery = kolQuery.Where(accgroup => filter.AccGroups.Contains(accgroup.AccGroupId));
            }

            if (!string.IsNullOrEmpty(filter.Title))
                kolQuery = kolQuery.Where(s => s.KolName.Contains(filter.Title));

            if (filter.AccKolCode > 0 && filter.AccKolCode != null)

                kolQuery = kolQuery.Where(s => s.AccKolCode.ToString().Substring(0, 2) == filter.AccKolCode.ToString());


            var count = (int)Math.Ceiling(kolQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var kols = await kolQuery.Paging(pager).ToListAsync();

            return filter.SetKol(kols).SetPaging(pager);
        }
       
        #endregion

        #region Dispose
        public void Dispose()
        {
            KolRepository?.Dispose();
          
            moeinRepository?.Dispose();
        }

      







        #endregion
    }
}
