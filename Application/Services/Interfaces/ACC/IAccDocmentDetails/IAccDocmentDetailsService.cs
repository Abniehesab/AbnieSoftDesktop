using Application.DTOS.ACC.AccDocmentDetails;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ACC.IAccDocmentDetails
{
    public interface IAccDocmentDetailsService
    {
        #region AccDocmentDetails
        Task<FilterAccDocmentDetailsDTO> FilterAccDocmentDetails(FilterAccDocmentDetailsDTO filter);
        Task<List<AccDocumentRowDataDTO>> GetDebtorAndCreditorByKolId(long kolId);
        Task<int> CalculateNatureFinalBalance(decimal totalDebtor, decimal totalCreditor);
       

        #endregion
    }
}
