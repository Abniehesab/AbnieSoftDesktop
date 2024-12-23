using Application.DTOS.PRO.Material;
using Application.Services.Interfaces.PRO.IMaterial;
using Common.Utilities.Paging;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations.PRO.Material
{
    public class MaterialService : IMaterialService
    {
        #region constructor
        private IGenericRepository<Domain.Entities.PRO.Material> _MaterialRepository;
        private IGenericRepository<Domain.Entities.PRO.MaterialUnit> _MaterialUnitRepository;
        private IGenericRepository<Domain.Entities.PRO.MaterialGroup> _MaterialGroupRepository;

        public MaterialService(IGenericRepository<Domain.Entities.PRO.Material> MaterialRepository, IGenericRepository<Domain.Entities.PRO.MaterialUnit> materialUnitRepository, IGenericRepository<Domain.Entities.PRO.MaterialGroup> materialGroupRepository)
        {
            _MaterialRepository = MaterialRepository;
            _MaterialUnitRepository = materialUnitRepository;
            _MaterialGroupRepository = materialGroupRepository;
        }

        #endregion   

        public async Task<List<Domain.Entities.PRO.Material>> GetMaterialByBusinessId(long BusinessId)
        {
            return await EFCore.ToListAsync(_MaterialRepository.GetEntitiesQuery()
             .Where(material => material.BusinessId == BusinessId && !material.IsDelete));
        }       

        public async Task<FilterMaterialDTO> FilterMaterial(FilterMaterialDTO filter)
        {
            var materialQuery = _MaterialRepository.GetEntitiesQuery()
             
                .Join(
                            _MaterialUnitRepository.GetEntitiesQuery(),
                            material => material.MaterialUnitId,
                            materialUnit => materialUnit.Id,
                            (material, materialUnit) => new { Material = material, MaterialUnit = materialUnit }
                )
                .Join(

                             _MaterialGroupRepository.GetEntitiesQuery(),
                            material => material.Material.MaterialGroupId,
                            materialGroup => materialGroup.Id,
                            (material, materialGroup) => new { Material = material, MaterialGroup = materialGroup }
                )
                .Select(materialQuery => new MaterialDTO
                {
                    Id = materialQuery.Material.Material.Id,
                    BusinessId = filter.BusinessId,
                    MaterialType= materialQuery.Material.Material.MaterialType,
                    MaterialCode = materialQuery.Material.Material.MaterialCode,
                    MaterialTitle = materialQuery.Material.Material.MaterialTitle,
                   
                    MaterialUnitId = materialQuery.Material.Material.MaterialUnitId,
                    MaterialUnitTitle = materialQuery.Material.MaterialUnit.MaterialUnitTitle,

                    MaterialGroupId= materialQuery.Material.Material.MaterialGroupId,
                    MaterialGroupTitle=materialQuery.MaterialGroup.MaterialGroupTitle,
                    MaterialDescription = materialQuery.Material.Material.MaterialDescription

                }).AsQueryable();

            switch (filter.OrderBy)
            {
                case (DTOS.PRO.MaterialGroup.MaterialGroupOrderBy?)MaterialOrderBy.CodeAsc:
                    materialQuery = materialQuery.OrderBy(s => s.Id);
                    break;
                case (DTOS.PRO.MaterialGroup.MaterialGroupOrderBy?)MaterialOrderBy.CodeDec:
                    materialQuery = materialQuery.OrderByDescending(s => s.Id);
                    break;
            }
            if (!string.IsNullOrEmpty(filter.Title))
                materialQuery = materialQuery.Where(s => s.MaterialTitle.Contains(filter.Title));

            if (filter.MaterialCode > 0 && filter.MaterialCode != null)

                materialQuery = materialQuery.Where(s => s.MaterialCode == filter.MaterialCode);

            if (filter.MaterialGroupId > 0 && filter.MaterialGroupId != null)

                materialQuery = materialQuery.Where(s => s.MaterialGroupId == filter.MaterialGroupId);


            if (!string.IsNullOrEmpty(filter.MaterialGroupTitle))
                materialQuery=materialQuery.Where(s=>s.MaterialGroupTitle.Contains(filter.MaterialGroupTitle));

            if (!string.IsNullOrEmpty(filter.MaterialDescription))
                materialQuery = materialQuery.Where(s => s.MaterialDescription.Contains(filter.MaterialDescription));

            var count = (int)Math.Ceiling(materialQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var material = materialQuery.Paging(pager).ToList();

            return filter.SetMaterial(material).SetPaging(pager);

        }


        public async Task<Domain.Entities.PRO.Material> GetMaterials(long MaterialId)
        {
            return await _MaterialRepository.GetEntitiesQuery().AsQueryable().
             SingleOrDefaultAsync(material => !material.IsDelete && material.Id == MaterialId);
        }

    
    }
}
