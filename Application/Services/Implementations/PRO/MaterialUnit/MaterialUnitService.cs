using Application.DTOS.PRO.MaterialUnit;
using Application.Services.Interfaces.PRO.IMaterialUnit;
using Common.Utilities.Paging;
using Domain.Entities.ACC;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;


namespace Application.Services.Implementations.PRO.MaterialUnit
{
    public class MaterialUnitService : IMaterialUnitService
    {

        #region constructor
        private IGenericRepository<Domain.Entities.PRO.MaterialUnit> _MaterialUnitRepository;

        public MaterialUnitService(IGenericRepository<Domain.Entities.PRO.MaterialUnit> materialUnitRepository)
        {
            _MaterialUnitRepository = materialUnitRepository;
        }

        #endregion
     

        public async Task<List<Domain.Entities.PRO.MaterialUnit>> GetMaterialUnitByBusinessId(long BusinessId)
        {
            return await EFCore.ToListAsync(_MaterialUnitRepository.GetEntitiesQuery()
             .Where(materialUnit => materialUnit.BusinessId == BusinessId && !materialUnit.IsDelete));
        }


 

        public async Task<FilterMaterialUnitDTO> FilterMaterialUnit(FilterMaterialUnitDTO filter)
        {
            var materialUnitQuery = _MaterialUnitRepository.GetEntitiesQuery()
                .Where(materialUnit => materialUnit.IsDelete == false && materialUnit.BusinessId == filter.BusinessId)
                .Select(materialUnitQuery => new MaterialUnitDTO
                {
                    Id = materialUnitQuery.Id,
                    BusinessId = filter.BusinessId,
                    MaterialUnitCode = materialUnitQuery.MaterialUnitCode,
                    MaterialUnitTitle = materialUnitQuery.MaterialUnitTitle
                }).AsQueryable();

            switch (filter.OrderBy)
            {
                case MaterialUnitOrderBy.CodeAsc:
                    materialUnitQuery = materialUnitQuery.OrderBy(s => s.MaterialUnitCode);
                    break;
                case MaterialUnitOrderBy.CodeDec:
                    materialUnitQuery = materialUnitQuery.OrderByDescending(s => s.MaterialUnitCode);
                    break;
            }
            if (!string.IsNullOrEmpty(filter.Title))
                materialUnitQuery = materialUnitQuery.Where(s => s.MaterialUnitTitle.Contains(filter.Title));

            if (filter.MaterialUnitCode > 0 && filter.MaterialUnitCode != null)

                materialUnitQuery = materialUnitQuery.Where(s => s.MaterialUnitCode == filter.MaterialUnitCode);

            var count = (int)Math.Ceiling(materialUnitQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var materialUnit = await EFCore.ToListAsync(materialUnitQuery.Paging(pager));

            return filter.SetMaterialUnit(materialUnit).SetPaging(pager);

        }



        public async Task<Domain.Entities.PRO.MaterialUnit> GetMaterialUnits(long MaterialUnitId)
        {
            return await _MaterialUnitRepository.GetEntitiesQuery().AsQueryable().
             SingleOrDefaultAsync(materialUnit => !materialUnit.IsDelete && materialUnit.Id == MaterialUnitId);
        }

        
     
    }
}
