using Application.DTOS.PRO.Contract;

using Application.Services.Interfaces.PRO.IContract;
using Common.Utilities.Paging;
using Domain.Entities.ACC;

using Domain.Entities.PRO;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Application.Services.Implementations.PRO.Contract
{
    public class ContractService : IContractService 
    {
        #region constructor
        private IGenericRepository<Domain.Entities.PRO.Contract> _ContractRepository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili> _TafziliRepository;
        private IGenericRepository<Domain.Entities.PRO.Tender> _TenderRepository;
        private IGenericRepository<Domain.Entities.ACC.Moein> _MoeinRepository;


        public ContractService(IGenericRepository<Domain.Entities.PRO.Contract> ContractRepository, IGenericRepository<Domain.Entities.ACC.Tafzili> TafziliRepository, IGenericRepository<Domain.Entities.PRO.Tender> tenderRepository, IGenericRepository<Domain.Entities.ACC.Moein> moeinRepository)
        {
            _ContractRepository = ContractRepository;
            _TafziliRepository = TafziliRepository;
            _TenderRepository = tenderRepository;
            _MoeinRepository = moeinRepository;
            
        }

        #endregion
      
        public async Task<List<Domain.Entities.PRO.Contract>> GetContractByBusinessId(long BusinessId)
        {
            return await EFCore.ToListAsync(_ContractRepository.GetEntitiesQuery()
             .Where(contract => contract.BusinessId == BusinessId && !contract.IsDelete));
        }


 
        public async Task<FilterContractDTO> FilterContract(FilterContractDTO filter)
        {

          
            var contractQuery = 
                ( from Contract in _ContractRepository.GetEntitiesQuery()                  

                  join tender in _TenderRepository.GetEntitiesQuery() on Contract.TenderId equals tender.Id into tenders
                  from tender in tenders.DefaultIfEmpty()


                  join tafzili in _TafziliRepository.GetEntitiesQuery() on Contract.ContractorId equals tafzili.Id into tafzilis
                  from tafzili in tafzilis.DefaultIfEmpty()


                  join moein in _MoeinRepository.GetEntitiesQuery() on Contract.MoeinId equals moein.Id into moeins
                  from moein in moeins.DefaultIfEmpty()

                  select new ContractDTO
                  {
                      ContractTitle = Contract.ContractTitle,
                      ContractLocation = Contract.ContractLocation,
                      ContractStartDate = Contract.ContractStartDate,
                      Id = Contract.Id,
                      BusinessId = (long)Contract.BusinessId,
                      ContractDate = Contract.ContractDate,
                      ContractCode = Contract.ContractCode,
                      ContractNumber = Contract.ContractNumber,
                      ContractType = Contract.ContractType,
                      ContractEndDate = Contract.ContractEndDate,
                      ContractPeriod = Contract.ContractPeriod,
                      ContractPrice = Contract.ContractPrice,
                      TerminationDate = Contract.TerminationDate,
                      ContractAgreements = Contract.ContractAgreements,
                      CostEstimation = Contract.CostEstimation,
                      PercentageOfChanges = Contract.PercentageOfChanges,
                      TenderId = Contract.TenderId,
                      TenderTitle = tender != null ? tender.TenderTitle : null,
                      ContractorId = Contract.ContractorId,
                      ContractorTitle = tafzili != null ? tafzili.TafziliName : null,
                      MoeinId = Contract.MoeinId,
                      MoeinTitle = moein != null ? moein.MoeinName : null
                  }).AsQueryable();
            contractQuery.ToList();

            switch (filter.OrderBy)
            {
                case ContractOrderBy.CodeAsc:
                    contractQuery = contractQuery.OrderBy(s => s.ContractCode);
                    break;
                case ContractOrderBy.CodeDec:
                    contractQuery = contractQuery.OrderByDescending(s => s.ContractCode);
                    break;
            }
            if (!string.IsNullOrEmpty(filter.Title))
                contractQuery = contractQuery.Where(s => s.ContractTitle.Contains(filter.Title));

            if (filter.ContractCode > 0 && filter.ContractCode != null)

                contractQuery = contractQuery.Where(s => s.ContractCode == filter.ContractCode);

            if (filter.ContractNumber > 0 && filter.ContractNumber != null)

                contractQuery = contractQuery.Where(s => s.ContractNumber == filter.ContractNumber);

            if (filter.ContractType > 0 && filter.ContractType != null)

                contractQuery = contractQuery.Where(s => s.ContractType == filter.ContractType);



            var count = (int)Math.Ceiling(contractQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var contract = contractQuery.Paging(pager).ToList();

            return filter.SetContract(contract).SetPaging(pager);

        }

        public async void test(FilterContractDTO filter)
        {
            try
            {
                var contractQuery = from Contract in _ContractRepository.GetEntitiesQuery()
                                    where Contract.BusinessId == filter.BusinessId && !Contract.IsDelete
                                    select Contract;

                var contracts = contractQuery.ToList();  // اینجا خطا را می‌بینید

                Debug.WriteLine($"Found {contracts.Count} contracts.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }

        public async Task<Domain.Entities.PRO.Contract> GetContracts(long ContractId)
        {
            return await _ContractRepository.GetEntitiesQuery().AsQueryable().
             SingleOrDefaultAsync(contract => !contract.IsDelete && contract.Id == ContractId);
        }
        public async Task<Domain.Entities.PRO.Contract> GetContractsByMoeinId(long MoeinId)
        {
            return await _ContractRepository.GetEntitiesQuery().AsQueryable().
             SingleOrDefaultAsync(contract => !contract.IsDelete && contract.MoeinId == MoeinId);
        }

  
      
    }
}
