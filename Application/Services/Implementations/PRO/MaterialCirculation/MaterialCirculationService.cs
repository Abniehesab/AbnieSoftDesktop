
using Application.DTOS.PRO.MaterialCirculation;
using Application.Services.Interfaces.PRO.IMaterialCirculation;
using Common.Utilities.Paging;
using Domain.Entities.ACC;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.PRO.MaterialCirculation
{
    public class MaterialCirculationService : IMaterialCirculationService
    {
        #region constructor
        private readonly IGenericRepository<Domain.Entities.PRO.MaterialCirculation> _materialCirculationRepository;
        private readonly IGenericRepository<Domain.Entities.PRO.MaterialGroup> _materialGroupRepository;
        private readonly IGenericRepository<Domain.Entities.PRO.Material> _materialRepository;
        private readonly IGenericRepository<Domain.Entities.PRO.MaterialUnit> _materialUnitRepository;
        private readonly IGenericRepository<Domain.Entities.PRO.Store> _storeRepository;
        private IGenericRepository<Domain.Entities.PRO.Contract> _contractRepository;
        private IGenericRepository<Domain.Entities.ACC.Tafzili> _tafziliRepository;


        public MaterialCirculationService(

          IGenericRepository<Domain.Entities.PRO.MaterialCirculation> materialCirculationRepository,
          IGenericRepository<Domain.Entities.PRO.MaterialGroup> materialGroupRepository,
          IGenericRepository<Domain.Entities.PRO.Material> materialRepository,
          IGenericRepository<Domain.Entities.PRO.MaterialUnit> materialUnitRepository,
          IGenericRepository<Domain.Entities.PRO.Store> storeRepository,
          IGenericRepository<Domain.Entities.PRO.Contract> contractRepository,
          IGenericRepository<Domain.Entities.ACC.Tafzili> tafziliRepository
           )
        {
            _materialCirculationRepository = materialCirculationRepository;
            _materialGroupRepository = materialGroupRepository;
            _materialRepository = materialRepository;
            _materialUnitRepository = materialUnitRepository;
            _storeRepository = storeRepository;
            _tafziliRepository = tafziliRepository;
            _contractRepository = contractRepository;
        }

        #endregion
        public async Task<FilterMaterialCirculationDTO> FilterMaterialCirculation(FilterMaterialCirculationDTO filter)
        {
            try
            {

                var MaterialCirculationQuery =
                    (from materialCirculation in _materialCirculationRepository.GetEntitiesQuery()
                    


                     join supplier in _tafziliRepository.GetEntitiesQuery() on materialCirculation.SupplierId equals supplier.Id into Suppliers
                     from supplier in Suppliers.DefaultIfEmpty()

                     join contract in _contractRepository.GetEntitiesQuery() on materialCirculation.ContractId equals contract.Id into Contracts
                     from contract in Contracts.DefaultIfEmpty()

                     join materialGroup in _materialGroupRepository.GetEntitiesQuery() on materialCirculation.MaterialGroupId equals materialGroup.Id into materialGroups
                     from materialGroup in materialGroups.DefaultIfEmpty()

                     join material in _materialRepository.GetEntitiesQuery() on materialCirculation.MaterialId equals material.Id into materials
                     from material in materials.DefaultIfEmpty()

                     join materialUnit in _materialUnitRepository.GetEntitiesQuery() on materialCirculation.MaterialUnitId equals materialUnit.Id into materialUnits
                     from materialUnit in materialUnits.DefaultIfEmpty()

                     join store in _storeRepository.GetEntitiesQuery() on materialCirculation.StoreId equals store.Id into stores
                     from store in stores.DefaultIfEmpty()


                         // Continue the pattern for other joins
                     select new MaterialCirculationDTO
                     {
                         Id = materialCirculation.Id,
                         BusinessId = (long)materialCirculation.BusinessId,
                         MaterialCirculationType = materialCirculation.MaterialCirculationType,
                         MaterialCirculationRowNumber = materialCirculation.MaterialCirculationRowNumber,
                         MaterialCirculationRowDate = materialCirculation.MaterialCirculationRowDate,
                         MaterialCirculationOperationNumber = materialCirculation.MaterialCirculationOperationNumber,
                         //MaterialType = materialCirculation.Material.MaterialType != null ? materialCirculation.Material.MaterialType : 0,
                         AmountMaterialEntered = materialCirculation.AmountMaterialEntered,
                         PriceMaterialEntered = materialCirculation.PriceMaterialEntered,
                         AmountMaterialOutput = materialCirculation.AmountMaterialOutput,
                         PriceMaterialOutput = materialCirculation.PriceMaterialOutput,
                         MaterialCirculationRowDescription = materialCirculation.MaterialCirculationRowDescription,

                         ContractId = materialCirculation.ContractId != null ? materialCirculation.ContractId : 0,
                         ContractTitle = materialCirculation.ContractId != null ? contract.ContractTitle : "",

                         BuyFactorId = materialCirculation.BuyFactorId,

                         MaterialGroupId = materialCirculation.MaterialGroupId,
                         MaterialGroupTitle = materialCirculation.MaterialGroupId != null ? materialGroup.MaterialGroupTitle : "",

                         MaterialId = materialCirculation.MaterialId,
                         MaterialTitle = materialCirculation.MaterialId != null ? material.MaterialTitle : "",

                         MaterialUnitId = materialCirculation.MaterialUnitId,
                         MaterialUnitTitle = materialCirculation.MaterialUnitId != null ? materialUnit.MaterialUnitTitle : "",

                         AccDocumentId = materialCirculation.AccDocumentId != null ? materialCirculation.AccDocumentId : 0,

                         SupplierId = materialCirculation.SupplierId != null ? materialCirculation.SupplierId : 0,
                         SupplierTitle = materialCirculation.SupplierId != null ? supplier.TafziliName : "",


                         StoreId = materialCirculation.StoreId != null ? materialCirculation.StoreId : 0,
                         StoreTitle = materialCirculation.StoreId != null ? store.StoreTitle : "",





                     })
                                         .OrderBy(materialCirculation => materialCirculation.MaterialCirculationRowDate)
                                         .ThenBy(materialCirculation => materialCirculation.MaterialCirculationOperationNumber)
                                         .AsQueryable();

                if (!string.IsNullOrEmpty(filter.MaterialCirculationRowDateStart.ToString()))
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialCirculationRowDate.Value.Date >= filter.MaterialCirculationRowDateStart.Value.Date);
                }

                if (!string.IsNullOrEmpty(filter.MaterialCirculationRowDateEnd.ToString()))
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialCirculationRowDate.Value.Date <= filter.MaterialCirculationRowDateEnd.Value.Date);
                }

                
                if (filter.SupplierId.HasValue && filter.SupplierId.Value != 0)
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.SupplierId == filter.SupplierId);
                }

                if (filter.ContractId.HasValue && filter.ContractId.Value != 0)
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.ContractId == filter.ContractId);
                }

                if (filter.StoreId.HasValue && filter.StoreId.Value != 0)
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.StoreId == filter.StoreId);
                }

                if (filter.MaterialGroupId.HasValue && filter.MaterialGroupId.Value != 0)
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialGroupId == filter.MaterialGroupId);
                }
                if (filter.MaterialId.HasValue && filter.MaterialId.Value != 0)
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialId == filter.MaterialId);
                }
                if (filter.MaterialCirculationType.HasValue && filter.MaterialCirculationType.Value !=0)
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialCirculationType == filter.MaterialCirculationType);
                }

                if (filter.MaterialType.HasValue && filter.MaterialType.Value <2)
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialType == filter.MaterialType);
                }

                if ((filter.MaterialCirculationOperationNumber.HasValue && filter.MaterialCirculationOperationNumber.Value != 0))
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialCirculationOperationNumber == filter.MaterialCirculationOperationNumber);
                }

                if (!string.IsNullOrEmpty(filter.MaterialCirculationRowDescription))
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialCirculationRowDescription == filter.MaterialCirculationRowDescription);
                }

                if (!string.IsNullOrEmpty(filter.MaterialCirculationRowDate.ToString()))
                {
                    MaterialCirculationQuery = MaterialCirculationQuery.Where(MaterialCirculationDetails => MaterialCirculationDetails.MaterialCirculationRowDate == filter.MaterialCirculationRowDate);
                }

           
                var acc = MaterialCirculationQuery.ToList();
                // محاسبات بر روی لیست مربوطه

                var count = (int)Math.Ceiling(MaterialCirculationQuery.Count() / (double)filter.TakeEntity);
                var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

                var MaterialCirculationsDetails = MaterialCirculationQuery.Paging(pager).ToList();



                return filter.SetMaterialCirculations(MaterialCirculationsDetails).SetPaging(pager);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
