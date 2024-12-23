using Application.DTOS.ACC.Kol;
using Application.DTOS.ACC.Tazili;
using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.ITafzili
{
    public interface ITafziliService
    {
        #region TAFZILI
      
        Task<FiltercostListDetailsDTO> FilterTafzili(FiltercostListDetailsDTO filter);
        Task<Tafzili> GetTafzilis(long TafziliId);
        Task<List<Tafzili>> GetTafziliByBusinessId(long BusinessId);
        Task<List<Tafzili>> GetTafzili2ByBusinessId(long BusinessId);
        Task<List<Tafzili>> GetTafzili3ByBusinessId(long BusinessId);
  
        Task<long> GetTafziliBy(int TafziliCode, long BusinessId);


      
        Task<bool> ExistsTafziliWithTafziliGroup(long TafziliGroupId);
        Task<bool> ExistsTafziliGroupWithTafzili(long TafziliGroupId);
        Task<long?> GetTafziliId(int AccTafziliCode, long BusinessId);
     
        Task<List<Tafzili>> GetTafziliByMoein(List<long> MoeinIds);

      
       

      

      
        #endregion
    }
}
