using Application.DTOS.PRO.MaterialCirculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.IMaterialCirculation
{
    public interface IMaterialCirculationService
    {
        #region
        Task<FilterMaterialCirculationDTO> FilterMaterialCirculation(FilterMaterialCirculationDTO filter);

        #endregion
    }
}
