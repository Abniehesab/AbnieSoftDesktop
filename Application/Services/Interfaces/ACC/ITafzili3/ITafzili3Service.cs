using Application.DTOS.ACC.Tafzili3;
using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.ITafzili3
{
    public interface ITafzili3Service
    {
        #region TAFZILI3


  
        Task<FilterTafzili3DTO> FilterTafzili3(FilterTafzili3DTO filter);
        Task<Tafzili3> GetTafzili3s(long Tafzili3Id);
        Task<List<Tafzili3>> GetTafzili3ByBusinessId(long BusinessId);
    
     
    
        #endregion

    }
}
