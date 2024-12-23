using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostListDetails
{
    public class EditCostListDetailsDTO
    {
        public long Id { get; set; }
        public long? BusinessId { set; get; }
        public DateTime? CostListRowDate { get; set; }
        public long? FK_AccDocumentRowLastModifierId { get; set; }
        public long? AccDocumentId { set; get; }
        public int? CostListNumber { get; set; }
        public long? KolId { get; set; }
        public long? MoeinId { get; set; }
        public long? TafziliId { get; set; }
        public long? Tafzili2Id { get; set; }
        public long? Tafzili3Id { get; set; }
        public long? SupplierId { get; set; }
        public long? MoeinForSupplierId { get; set; }
        public long? BankId { get; set; }
        public string? ReceiptNumber { get; set; }

        public long? MaterialGroupId { get; set; }
        public long? MaterialId { get; set; }
        public int? AmountMaterial { get; set; }
        public long? MaterialUnitId { get; set; }
        public decimal? PriceMaterial { get; set; }

        public string CostListRowDescription { get; set; }
        public decimal? CostListRowInitialValue { get; set; }
        public long? CostListFactorId { get; set; }
        public long? MoeinForSideFactorId { get; set; }
        public decimal? CostListRowfinalValue { get; set; }
    }
}
