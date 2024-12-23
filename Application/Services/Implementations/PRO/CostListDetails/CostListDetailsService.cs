using Application.DTOS.PRO.CostListDetails;
using Application.Services.Interfaces.PRO.ICostListDetails;
using Common.Utilities.Paging;

using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.PRO.CostListDetails
{
    public class CostListDetailsService : ICostListDetailsService
    {
        #region constructor
        private readonly IGenericRepository<Domain.Entities.PRO.CostListDetails> _costListDetailsRepository;
        private readonly IGenericRepository<Domain.Entities.PRO.MaterialGroup> _materialGroupRepository;
        private readonly IGenericRepository<Domain.Entities.PRO.Material> _materialRepository;
        private readonly IGenericRepository<Domain.Entities.PRO.MaterialUnit> _materialUnitRepository;
        private IGenericRepository<Domain.Entities.PRO.ProjectStatusFactor> _projectStatusFactorRepository;


        private IGenericRepository<Domain.Entities.ACC.Kol> _kolRepository;
        private IGenericRepository<Domain.Entities.ACC.Moein>_moeinRepository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili> _tafziliRepository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili2>_tafzili2Repository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili3> _tafzili3Repository;

        public CostListDetailsService(

          IGenericRepository<Domain.Entities.PRO.CostListDetails> costListDetailsRepository,
          IGenericRepository<Domain.Entities.PRO.MaterialGroup> materialGroupRepository,
          IGenericRepository<Domain.Entities.PRO.Material> materialRepository,
          IGenericRepository<Domain.Entities.PRO.MaterialUnit> materialUnitRepository,
          IGenericRepository<Domain.Entities.PRO.ProjectStatusFactor> projectStatusFactorRepository,
          IGenericRepository<Domain.Entities.ACC.Kol> kolRepository,
          IGenericRepository<Domain.Entities.ACC.Moein> moeinRepository,
          IGenericRepository<Domain.Entities.ACC.Tafzili> tafziliRepository,
          IGenericRepository<Domain.Entities.ACC.Tafzili2> tafzili2Repository,
          IGenericRepository<Domain.Entities.ACC.Tafzili3> tafzili3Repository
           )
        {
            _costListDetailsRepository = costListDetailsRepository;  
            _materialGroupRepository = materialGroupRepository;
            _materialRepository = materialRepository;
            _materialUnitRepository = materialUnitRepository;
            _kolRepository = kolRepository;
            _moeinRepository = moeinRepository; 
            _tafziliRepository = tafziliRepository;
            _tafzili2Repository = tafzili2Repository;
            _tafzili3Repository = tafzili3Repository;
            _projectStatusFactorRepository = projectStatusFactorRepository;
        }

        #endregion

        #region FilterCostListDetails
        public async Task<FilterCostListDetailsDTO> FilterCostListDetails(FilterCostListDetailsDTO filter)
        {
            try
            {

                var CostListsDetailQuery = (from costListDetails in _costListDetailsRepository.GetEntitiesQuery()
                                            
                                            join kol in _kolRepository.GetEntitiesQuery() on costListDetails.KolId equals kol.Id into kolGroup
                                            from kol in kolGroup.DefaultIfEmpty()

                                            join moein in _moeinRepository.GetEntitiesQuery() on costListDetails.MoeinId equals moein.Id into moeinGroup
                                            from moein in moeinGroup.DefaultIfEmpty()

                                            join tafzili in _tafziliRepository.GetEntitiesQuery() on costListDetails.TafziliId equals tafzili.Id into tafziliGroup
                                            from tafzili in tafziliGroup.DefaultIfEmpty()

                                            join supplier in _tafziliRepository.GetEntitiesQuery() on costListDetails.SupplierId equals supplier.Id into Suppliers
                                            from supplier in Suppliers.DefaultIfEmpty()

                                            join MoeinForsupplier in _moeinRepository.GetEntitiesQuery() on costListDetails.MoeinForSupplierId equals MoeinForsupplier.Id into MoeinForsuppliers
                                            from MoeinForsupplier in MoeinForsuppliers.DefaultIfEmpty()


                                            join sideFactor in _tafziliRepository.GetEntitiesQuery() on costListDetails.SideFactorId equals sideFactor.Id into sideFactors
                                            from sideFactor in sideFactors.DefaultIfEmpty()

                                            join MoeinForsideFactor in _moeinRepository.GetEntitiesQuery() on costListDetails.MoeinForSideFactorId equals MoeinForsideFactor.Id into MoeinForsideFactors
                                            from MoeinForsideFactor in MoeinForsideFactors.DefaultIfEmpty()

                                            join tafzili2 in _tafzili2Repository.GetEntitiesQuery() on costListDetails.Tafzili2Id equals tafzili2.Id into tafzili2Group
                                            from tafzili2 in tafzili2Group.DefaultIfEmpty()

                                            join tafzili3 in _tafzili3Repository.GetEntitiesQuery() on costListDetails.Tafzili3Id equals tafzili3.Id into tafzili3Group
                                            from tafzili3 in tafzili3Group.DefaultIfEmpty()

                                            join materialGroup in _materialGroupRepository.GetEntitiesQuery() on costListDetails.MaterialGroupId equals materialGroup.Id into materialGroups
                                            from materialGroup in materialGroups.DefaultIfEmpty()

                                            join material in _materialRepository.GetEntitiesQuery() on costListDetails.MaterialId equals material.Id into materials
                                            from material in materials.DefaultIfEmpty()

                                            join projectStatusFactor in _projectStatusFactorRepository.GetEntitiesQuery() on costListDetails.CostListFactorId equals projectStatusFactor.Id into projectStatusFactors
                                            from projectStatusFactor in projectStatusFactors.DefaultIfEmpty()

                                            join materialUnit in _materialUnitRepository.GetEntitiesQuery() on costListDetails.MaterialUnitId equals materialUnit.Id into materialUnits
                                            from materialUnit in materialUnits.DefaultIfEmpty()
                                                // Continue the pattern for other joins
                                            select new CostListDetailsDTO
                                            {
                                                Id = costListDetails.Id,
                                                CostListNumber = costListDetails.CostListNumber,
                                                BusinessId = costListDetails.BusinessId,
                                                CostListRowDate = costListDetails.CostListRowDate,
                                                CostListRowDescription = costListDetails.CostListRowDescription,
                                                CostListRowNumber = costListDetails.CostListRowNumber,
                                                FK_AccDocumentRowLastModifierId = costListDetails.FK_AccDocumentRowLastModifierId,
                                                AccDocumentId = costListDetails.AccDocumentId,
                                                AmountMaterial = costListDetails.AmountMaterial,
                                                PriceMaterial = costListDetails.PriceMaterial,
                                                CostListRowInitialValue = costListDetails.CostListRowInitialValue,
                                                CostListRowfinalValue = costListDetails.CostListRowfinalValue,

                                                KolId = kol != null ? kol.Id : 0,
                                                KolName = kol != null ? kol.KolName : "",

                                                MoeinId = moein != null ? moein.Id:0,
                                                MoeinName = moein != null ? moein.MoeinName : "",

                                                TafziliId = tafzili != null ? tafzili.Id : 0,
                                                TafziliName = tafzili != null ? tafzili.TafziliName : "",


                                                Tafzili2Id = tafzili2 != null ? tafzili2.Id : 0,
                                                Tafzili2Name = tafzili2 != null ? tafzili2.Tafzili2Name : "",

                                                Tafzili3Id = tafzili3 != null ? tafzili3.Id : 0,
                                                Tafzili3Name = tafzili3 != null ? tafzili3.Tafzili3Name : "",

                                                MaterialGroupId = materialGroup != null ? materialGroup.Id : 0,
                                                MaterialGroupTitle = materialGroup != null ? materialGroup.MaterialGroupTitle : "",

                                                MaterialId = material != null ? material.Id : 0,
                                                MaterialTitle = material != null ? material.MaterialTitle : "",

                                                CostListFactorId = projectStatusFactor != null ? projectStatusFactor.Id : 0,
                                                CostListFactorPercent = projectStatusFactor != null ? projectStatusFactor.ProjectStatusFactorPercent : 0,
                                                CostListFactorTitle = projectStatusFactor != null ? projectStatusFactor.ProjectStatusFactorTitle : "",

                                                MaterialUnitId = materialUnit != null ? materialUnit.Id : 0,
                                                MaterialUnitTitle = materialUnit != null ? materialUnit.MaterialUnitTitle : "",

                                                SupplierId = supplier != null ? supplier.Id : 0,
                                                SupplierTitle = supplier != null ? supplier.TafziliName : "",

                                                MoeinForSupplierId = MoeinForsupplier != null ? MoeinForsupplier.Id : 0,
                                                MoeinForSupplierTitle = MoeinForsupplier != null ? MoeinForsupplier.MoeinName : "",

                                                SideFactorId = sideFactor != null ? sideFactor.Id : 0,
                                                SideFactorTitle = sideFactor != null ? sideFactor.TafziliName : "",
                                                MoeinForSideFactorId = MoeinForsideFactor != null ? MoeinForsideFactor.Id : 0,
                                                MoeinForSideFactorTitle = MoeinForsideFactor != null ? MoeinForsideFactor.MoeinName : "",



                                            })
                                         .OrderBy(CostListDetails => CostListDetails.CostListRowDate)
                                         .ThenBy(CostListDetails => CostListDetails.CostListNumber)
                                         .AsQueryable();
                var acc2 = CostListsDetailQuery.ToList();

                if (!string.IsNullOrEmpty(filter.CostListRowDateStart.ToString()))
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.CostListRowDate.Value.Date >= filter.CostListRowDateStart.Value.Date);
                }

                if (!string.IsNullOrEmpty(filter.CostListRowDateEnd.ToString()))
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.CostListRowDate.Value.Date <= filter.CostListRowDateEnd.Value.Date);
                }

                if (filter.KolId.HasValue && filter.KolId.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.KolId == filter.KolId);
                }

                if (filter.MoeinId.HasValue && filter.MoeinId.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.MoeinId == filter.MoeinId);
                }

                if (filter.TafziliId.HasValue && filter.TafziliId.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.TafziliId == filter.TafziliId);
                }

                if (filter.Tafzili2Id.HasValue && filter.Tafzili2Id.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.Tafzili2Id == filter.Tafzili2Id);
                }

                if (filter.Tafzili3Id.HasValue && filter.Tafzili3Id.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.Tafzili3Id == filter.Tafzili3Id);
                }


                if (filter.SupplierId.HasValue && filter.SupplierId.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.SupplierId == filter.SupplierId);
                }

                if (filter.MaterialGroupId.HasValue && filter.MaterialGroupId.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.MaterialGroupId == filter.MaterialGroupId);
                }
                if (filter.MaterialId.HasValue && filter.MaterialId.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.MaterialId == filter.MaterialId);
                }

                if (filter.SideFactorId.HasValue && filter.SideFactorId.Value != 0)
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.SideFactorId == filter.SideFactorId);
                }

                if ((filter.CostListNumber.HasValue && filter.CostListNumber.Value != 0))
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.CostListNumber == filter.CostListNumber);
                }

                if (!string.IsNullOrEmpty(filter.CostListRowDescription))
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.CostListRowDescription == filter.CostListRowDescription);
                }

                if (!string.IsNullOrEmpty(filter.CostListRowDate.ToString()))
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.CostListRowDate == filter.CostListRowDate);
                }

                if ((filter.CostListRowfinalValue.HasValue && filter.CostListRowfinalValue.Value != 0))
                {
                    CostListsDetailQuery = CostListsDetailQuery.Where(CostListDetails => CostListDetails.CostListRowfinalValue == filter.CostListRowfinalValue);
                }

                var acc = CostListsDetailQuery.ToList();
                // محاسبات بر روی لیست مربوطه

                var count = (int)Math.Ceiling(CostListsDetailQuery.Count() / (double)filter.TakeEntity);
                var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

                var CostListsDetails = CostListsDetailQuery.Paging(pager).ToList();



                return filter.SetCostListDetails(CostListsDetails).SetPaging(pager);

            }
            catch (Exception ex)
            {
                return null;
            }
            


        }
        #endregion
    }
}
