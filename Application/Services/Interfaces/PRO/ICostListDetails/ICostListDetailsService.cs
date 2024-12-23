using Application.DTOS.PRO.CostListDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.ICostListDetails
{
    public interface ICostListDetailsService
    {
        #region CostListDetails

        Task<FilterCostListDetailsDTO> FilterCostListDetails(FilterCostListDetailsDTO filter);


        #endregion
    }
}
