using Application.DTOS.ACC.Kol;
using Application.DTOS.ACC.TafziliGroup;
using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.ITafziliGroup
{
    public interface ITafziliGroupService
    {
        #region TafziliGroup
      

        Task<FilterTafziliGroupDTO> FilterTafziliGroup(FilterTafziliGroupDTO filter);
        Task<TafziliGroup> GetTafziliGroups(long TafziliGroupId);
        Task<List<TafziliGroup>> GetTafziliGroupByBusinessId(long  BusinessId);

        Task<long> GetTafziliGroupByName(string TafziliGroupName, long BusinessId);

   
  
        Task<long?> GetTafziliGroupsByTafziliId(long TafziliId);
        Task<List<long?>> GetTafziliGroupByTafziliId(long TafziliId);
 


        #endregion
    }
}
