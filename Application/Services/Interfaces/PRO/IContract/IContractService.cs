using Application.DTOS.PRO.Contract;
using Domain.Entities.PRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.PRO.IContract
{
    public interface IContractService
    {
        #region Contract

      
        Task<FilterContractDTO> FilterContract(FilterContractDTO filter);
        Task<Contract> GetContracts(long ContractId);
        Task<Contract> GetContractsByMoeinId(long MoeinId);
        void test(FilterContractDTO filter);


        #endregion
    }
}
