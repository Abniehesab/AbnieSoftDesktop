using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostList
{
    public class CostListDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public long? FK_AccDocumentRowLastModifierId { get; set; }
        public long? AccDocumentId { set; get; }
        public DateTime? CostListDate { get; set; }
        public int? CostListCode { get; set; }
        public int? CostListNumber { get; set; }
        public int? CostListType { get; set; }
        public int? PaymentType { get; set; }
        public decimal? TotallyCostList { get; set; }
        public long? StoreId { get; set; }
        public long? InstantPaymentId { get; set; }
        public long? MoeinForInstantPaymentId { get; set; }
        public long? ContractId { get; set; }
        public string? ContractTitle { get; set; }
        public long? WorkshopId { get; set; }
        public string? WorkshopTitle { get; set; }
        public long? employerId { set; get; }
        public string? employerTitle { set; get; }
        public long? MoeinForemployerId { get; set; }
        public string? CostListDescription { get; set; }

    }
}
