using Application.DTOS.PRO.CostListDetails;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.MaterialCirculation
{
    public class FilterMaterialCirculationDTO : BasePaging
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? MaterialCirculationType { get; set; }
        public int? MaterialCirculationOperationNumber { get; set; }
        public DateTime? MaterialCirculationRowDate { get; set; }
        public DateTime? MaterialCirculationRowDateStart { get; set; }
        public DateTime? MaterialCirculationRowDateEnd { get; set; }
        public long? MaterialGroupId { get; set; }
        public string? MaterialGroupTitle { get; set; }
        public long? MaterialId { get; set; }
        public string? MaterialTitle { get; set; }
        public int?  MaterialType { get; set; }
        public long? ContractId { get; set; }
        public string? ContractTitle { get; set; }
        public long? CostListId { set; get; }
        public long? SupplierId { get; set; }
        public string? SupplierTitle { get; set; }
        public long? StoreId { get; set; }
        public string? StoreTitle { get; set; }
        public string? MaterialCirculationRowDescription { get; set; }
        public List<MaterialCirculationDTO>? MaterialCirculations { get; set; }
        public MaterialCirculationOrderBy? OrderBy { get; set; }
        public FilterMaterialCirculationDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            return this;
        }

        public FilterMaterialCirculationDTO SetMaterialCirculations(List<MaterialCirculationDTO> materialCirculations)
        {
            this.MaterialCirculations = materialCirculations;
            return this;
        }
    }
    public enum MaterialCirculationOrderBy
    {
        CodeAsc,
        CodeDec
    }

}
