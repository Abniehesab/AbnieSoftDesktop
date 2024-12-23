using Application.DTOS.Fin.ReceiveCheque;

using Domain.Entities.FIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.FIN.IReceive
{
    public interface IReceiveService
    {
        #region Receive                 
        Task<FilterReceiveChequeDTO> FilterReceiveCheques(FilterReceiveChequeDTO filter);
        
        Task<ReceiveCheque> GetReceiveCheque(long ReceiveChequeId);         
        #endregion
    }
}
