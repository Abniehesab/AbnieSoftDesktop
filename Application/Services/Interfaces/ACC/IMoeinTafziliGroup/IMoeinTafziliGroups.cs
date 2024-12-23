using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.IMoeinTafziliGroup
{
    public interface IMoeinTafziliGroups
    {
        Task<List<MoeinTafziliGroups>> GetMoeinTafziliGroupByCreatorId(long TafziliGroupId);
     
        Task<bool> ExistsMoeinTafziliGroupWithMoein(long MoeinId);

        Task<List<long>> TafziliGroupIdWithMoeinId(List<long> MoeinIds);
        Task<List<long>> MoeinIdWithTafziliGroupId(long TafziliGroupId);
    }
}
