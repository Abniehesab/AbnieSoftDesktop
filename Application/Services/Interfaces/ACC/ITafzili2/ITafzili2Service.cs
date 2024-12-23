using Application.DTOS.ACC.Tafzili2;
using Application.DTOS.ACC.Tazili;
using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.ITafzili2
{
    public interface ITafzili2Service
    {
        #region TAFZILI2
 
        Task<FilterTafzili2DTO> FilterTafzili2(FilterTafzili2DTO filter);
        Task<List<Tafzili2>> GetTafzili2ByBusinessId(long BusinessId);
   
        Task<Tafzili2> GetTafzili2s(long Tafzili2Id);
       
        #endregion

    }
}
