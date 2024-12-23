using Application.DTOS.PRO.MaterialGroup;
using Application.Services.Interfaces.PRO.MaterialGroup;
using Common.Utilities.Paging;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;


namespace Application.Services.Implementations.PRO.MaterialGroup
{
    public class MaterialGroupService : IMaterialGroupService
    {

        #region constructor
        private IGenericRepository<Domain.Entities.PRO.MaterialGroup> _MaterialGroupRepository;

        public MaterialGroupService(IGenericRepository<Domain.Entities.PRO.MaterialGroup> materialGroupRepository)
        {
            _MaterialGroupRepository = materialGroupRepository;
        }

        #endregion
  

        public async Task<List<Domain.Entities.PRO.MaterialGroup>> GetMaterialGroupByBusinessId(long BusinessId)
        {
            return await EFCore.ToListAsync(_MaterialGroupRepository.GetEntitiesQuery()
             .Where(materialGroup => materialGroup.BusinessId == BusinessId && !materialGroup.IsDelete));
        } 

        public async Task<FilterMaterialGroupDTO> FilterMaterialGroup(FilterMaterialGroupDTO filter)
        {
            var materialGroupQuery = _MaterialGroupRepository.GetEntitiesQuery()              
                .Select(materialGroupQuery => new MaterialGroupDTO
                {
                    Id = materialGroupQuery.Id,
                    BusinessId = filter.BusinessId,
                    MaterialGroupCode = materialGroupQuery.MaterialGroupCode,
                    MaterialGroupTitle = materialGroupQuery.MaterialGroupTitle
                }).AsQueryable();

            switch (filter.OrderBy)
            {
                case MaterialGroupOrderBy.CodeAsc:
                    materialGroupQuery = materialGroupQuery.OrderBy(s => s.MaterialGroupCode);
                    break;
                case MaterialGroupOrderBy.CodeDec:
                    materialGroupQuery = materialGroupQuery.OrderByDescending(s => s.MaterialGroupCode);
                    break;
            }
            if (!string.IsNullOrEmpty(filter.Title))
                materialGroupQuery = materialGroupQuery.Where(s => s.MaterialGroupTitle.Contains(filter.Title));

            if (filter.MaterialGroupCode > 0 && filter.MaterialGroupCode != null)

                materialGroupQuery = materialGroupQuery.Where(s => s.MaterialGroupCode == filter.MaterialGroupCode);

            var count = (int)Math.Ceiling(materialGroupQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var materialGroup = materialGroupQuery.Paging(pager).ToList();

            return filter.SetMaterialGroup(materialGroup).SetPaging(pager);

        }



        public async Task<Domain.Entities.PRO.MaterialGroup> GetMaterialGroups(long MaterialGroupId)
        {
            return await _MaterialGroupRepository.GetEntitiesQuery().AsQueryable().
             SingleOrDefaultAsync(materialGroup => !materialGroup.IsDelete && materialGroup.Id == MaterialGroupId);
        }
      
    }
}
