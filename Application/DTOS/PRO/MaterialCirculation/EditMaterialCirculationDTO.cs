using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.MaterialCirculation
{
    public class EditMaterialCirculationDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public long? FK_AccDocumentRowLastModifierId { get; set; }
        public long? AccDocumentId { set; get; }

        public int? MaterialCirculationType { get; set; }
        public int? MaterialCirculationOperationNumber { get; set; }
        public int? MaterialCirculationRowNumber { get; set; }
        public DateTime? MaterialCirculationRowDate { get; set; }
        public long? MaterialGroupId { get; set; }
        public long? MaterialId { get; set; }
        public long? MaterialUnitId { get; set; }
        public int? AmountMaterial { get; set; }
        public decimal? PriceMaterial { get; set; }
        public long? BuyFactorId { set; get; }
        public string MaterialCirculationRowDescription { get; set; }
        public bool IsUpdate { get; set; }
        public long? ContractId { get; set; }
        public long? CostListId { set; get; }
        public long? SupplierId { get; set; }
        public long? StoreId { get; set; }
    }
    public enum EditMaterialCirculationResult
    {
        NumberExists,
        Success,
        UnSuccess,
        CodeExists,
    }

}
