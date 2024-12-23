using Application.DTOS.PRO.MaterialUnit;
using Domain.Entities.PRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.IMaterialUnit
{
    public interface IMaterialUnitService
    {
        #region MaterialUnit

      
        Task<FilterMaterialUnitDTO> FilterMaterialUnit(FilterMaterialUnitDTO filter);

        Task<MaterialUnit> GetMaterialUnits(long MaterialUnitId);
        Task<List<MaterialUnit>> GetMaterialUnitByBusinessId(long BusinessId);
    

        #endregion
    }
}
