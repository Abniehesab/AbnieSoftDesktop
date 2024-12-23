using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostListDetails
{
    public class CostListDetailsDTO
    {
        public long Id { get; set; }
        public long? BusinessId { set; get; }
        public long? AccDocumentId { set; get; }
        public int? CostListNumber { get; set; }
        public int? CostListRowNumber { get; set; }
        public DateTime? CostListRowDate { get; set; }
        public long? FK_AccDocumentRowLastModifierId { get; set; }

        public long? KolId { get; set; }
        public string? KolName { get; set; }
        public long? MoeinId { get; set; }
        public string? MoeinName { get; set; }
        public long? TafziliId { get; set; }
        public string? TafziliName { get; set; }
        public long? Tafzili2Id { get; set; }
        public string? Tafzili2Name { get; set; }
        public long? Tafzili3Id { get; set; }
        public string? Tafzili3Name { get; set; }

        public long? SupplierId { get; set; }
        public string? SupplierTitle { get; set; }
        public long? MoeinForSupplierId { get; set; }
        public string? MoeinForSupplierTitle { get; set; }

        public long? BankId { get; set; }
        public string? BankTitle { get; set; }
        public string? ReceiptNumber { get; set; }

        public long? MaterialGroupId { get; set; }
        public string? MaterialGroupTitle { get; set; }
        public long? MaterialId { get; set; }
        public string? MaterialTitle { get; set; }
        public float? AmountMaterial { get; set; }
        public long? MaterialUnitId { get; set; }
        public string? MaterialUnitTitle { get; set; }
        public decimal? PriceMaterial { get; set; }     
        public string CostListRowDescription { get; set; }
        public decimal? CostListRowInitialValue { get; set; }
        public long? CostListFactorId { get; set; }
        public float? CostListFactorPercent { get; set; }
        public long? SideFactorId { get; set; }
        public string? SideFactorTitle { get; set; }
        public long? MoeinForSideFactorId { get; set; }
        public string? MoeinForSideFactorTitle { get; set; }
        public string? CostListFactorTitle { get; set; }
        public decimal? CostListRowfinalValue { get; set; }

        public int RowNumber { get; set; }

    }
}
