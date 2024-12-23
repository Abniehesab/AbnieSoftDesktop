
using Application.DTOS.Fin.PaymentCheque;

using Domain.Entities.FIN;
using Domain.Entities.PRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.FIN.IPayment
{
    public interface IPaymentService
    {
        #region Payment
        
        Task<PaymentCheque> GetPaymentCheque(long PaymentChequeId);
        Task<FilterPaymentChequeDTO> FilterPaymentCheques(FilterPaymentChequeDTO filter);

        #endregion
    }
}
