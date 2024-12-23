using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostList
{
    public class RegisterCostListDTO
    {
        public long BusinessId { get; set; }
        public long? AccDocumentId { set; get; }
        public DateTime? CostListDate { get; set; }
        public int? CostListNumber { get; set; }
        public int? CostListType { get; set; }
        public int? PaymentType { get; set; }
        public long? ContractId { get; set; }
        public decimal? TotallyCostList { get; set; }
        public long? InstantPaymentId { get; set; }
        public long? StoreId { get; set; }
        public long? MoeinForInstantPaymentId { get; set; }
        public long? WorkshopId { get; set; }
        public long? employerId { set; get; }
        public long? MoeinForemployerId { get; set; }
        public long? FK_AccDocumentLastModifierId { get; set; }
        public string? CostListDescription { get; set; }
    }
    public enum RegisterCostListResult
    {
        NumberExists,
        Success,
        UnSuccess,
        CodeExists,
    }
}
