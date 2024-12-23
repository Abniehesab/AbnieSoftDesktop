using Domain.Entities.ACC;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.MaterialCirculation
{
    public class MaterialCirculationDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public long? FK_MaterialCirculationRowLastModifierId { get; set; }
        public long? AccDocumentId { set; get; }

        public int? MaterialCirculationType { get; set; }
        public int? MaterialCirculationOperationNumber { get; set; }
        public int? MaterialCirculationRowNumber { get; set; }
        public DateTime? MaterialCirculationRowDate { get; set; }
        public long? MaterialGroupId { get; set; }
        public string? MaterialGroupTitle { get; set; }
        public long? MaterialId { get; set; }
        public string? MaterialTitle { get; set; }
        public int? MaterialType { get; set; }
        public long? MaterialUnitId { get; set; }
        public string? MaterialUnitTitle { get; set; }
        
        public float? AmountMaterialEntered { get; set; }        
        public decimal? PriceMaterialEntered { get; set; }        
        public float? AmountMaterialOutput { get; set; }        
        public decimal? PriceMaterialOutput { get; set; }

        public long? BuyFactorId { set; get; }
        public string? MaterialCirculationRowDescription { get; set; }
        public bool IsUpdate { get; set; }              
        public long? ContractId { get; set; }
        public string? ContractTitle { get; set; }
        public long? CostListId { set; get; }      
        public long? SupplierId { get; set; }
        public string? SupplierTitle { get; set; }
        public long? StoreId { get; set; }
        public string? StoreTitle { get; set; }

        public int RowNumber { get; set; }
    }
}
