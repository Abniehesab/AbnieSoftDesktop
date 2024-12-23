using Application.DTOS.PRO.ProjectStatusFactor;
using Domain.Entities.PRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.IProjectStatusFactor
{
    public interface IProjectStatusFactorService
    {
        #region ProjectStatusFactor

       
        Task<FilterProjectStatusFactorDTO> FilterProjectStatusFactor(FilterProjectStatusFactorDTO filter);

       
        #endregion
    }
}
