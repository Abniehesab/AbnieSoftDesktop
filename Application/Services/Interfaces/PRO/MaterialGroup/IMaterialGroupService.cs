using Application.DTOS.PRO.MaterialGroup;
using Domain.Entities.PRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.MaterialGroup
{
    public interface IMaterialGroupService
    {
        #region MaterialGroup      
        Task<FilterMaterialGroupDTO> FilterMaterialGroup(FilterMaterialGroupDTO filter);

        Task<Domain.Entities.PRO.MaterialGroup> GetMaterialGroups(long MaterialGroupId);
        Task<List<Domain.Entities.PRO.MaterialGroup>> GetMaterialGroupByBusinessId(long BusinessId);    

        #endregion
    }
}
