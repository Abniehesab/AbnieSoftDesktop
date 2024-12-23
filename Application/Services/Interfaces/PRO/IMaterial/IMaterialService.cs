using Application.DTOS.PRO.Material;
using Domain.Entities.PRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.IMaterial
{
    public interface IMaterialService
    {
        #region Material

    
        Task<FilterMaterialDTO> FilterMaterial(FilterMaterialDTO filter);

        Task<Material> GetMaterials(long MaterialId);
        Task<List<Material>> GetMaterialByBusinessId(long BusinessId);
     
        #endregion
    }
}
