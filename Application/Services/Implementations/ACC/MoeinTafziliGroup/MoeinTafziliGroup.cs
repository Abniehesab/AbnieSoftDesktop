using Application.Services.Interfaces.ACC.IMoeinTafziliGroup;
using Domain.Entities.ACC;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.ACC.MoeinTafziliGroup
{
    public class MoeinTafziliGroup : IMoeinTafziliGroups
    {
        #region constructor
        private IGenericRepository<Domain.Entities.ACC.MoeinTafziliGroups> MoeintafziligroupRepository;

   
        public MoeinTafziliGroup(IGenericRepository<Domain.Entities.ACC.MoeinTafziliGroups> moeintafziligroupRepository)
        {
            this.MoeintafziligroupRepository = moeintafziligroupRepository;
        }


        #endregion
        #region MoeinTafziliGroup Section
        public async Task<List<Domain.Entities.ACC.MoeinTafziliGroups>> GetMoeinTafziliGroupByCreatorId(long TafziliGroupId)
        {
            return await MoeintafziligroupRepository.GetEntitiesQuery()
                 .Where(moeinTafziliGroup => moeinTafziliGroup.TafziliGroupId== TafziliGroupId && !moeinTafziliGroup.IsDelete ).ToListAsync();

        }
                  

        public async Task<bool> ExistsMoeinTafziliGroupWithMoein(long MoeinId)
        {
            return await MoeintafziligroupRepository.GetEntitiesQuery()
                 .AnyAsync(moeintafziligroup => moeintafziligroup.MoeinId == MoeinId && !moeintafziligroup.IsDelete);
        }


        public async Task<List<long>> TafziliGroupIdWithMoeinId(List<long> MoeinIds)
        {
            return await MoeintafziligroupRepository.GetEntitiesQuery()
                .Where(moeinTafziliGroup => MoeinIds.Contains(moeinTafziliGroup.MoeinId))
                .Select(moeinTafziliGroup => moeinTafziliGroup.TafziliGroupId)
                .ToListAsync();
        }


        public async Task<List<long>> MoeinIdWithTafziliGroupId(long TafziliGroupId)
        {
            return await MoeintafziligroupRepository.GetEntitiesQuery()
             .Where(moeinTafziliGroup => moeinTafziliGroup.TafziliGroupId==TafziliGroupId)
             .Select(moeinTafziliGroup => moeinTafziliGroup.MoeinId)
             .ToListAsync();
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            MoeintafziligroupRepository?.Dispose();
        }


        #endregion
    }

}
