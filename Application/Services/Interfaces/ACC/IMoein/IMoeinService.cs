using Application.DTOS.ACC.Moein;
using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.IMoein
{
    public interface IMoeinService
    {
        #region Moein
        Task<List<Moein>> GetAllMoein();
      
        Task<FilterMoeinDTO> FilterMoein(FilterMoeinDTO filter);
        Task<Moein> GetMoeins(long MoeinId);
        Task<List<Moein>> GetMoeinsByIds(List<long> MoeinIds);
        Task<long> GetMoeinBy(int MoeinCode, long BusinessId);
        Task<long> GetMoeinByName(string MoeinName, long BusinessId);
        Task<List<Moein>> GetMoeinByBusinessId(long BusinessId);

        Task<List<Moein>> GetMoeinByKolId(long KolId);
        Task<List<Moein>> GetMoeinsByTafzili(long TafziliId);       
        bool IsMoeinExistCode(int MoeinCode);
        bool IsMoeinExistName(string MoeinName);
      

        #endregion
    }
}
