
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostList
{
    public class FilterCostListDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public int? CostListNumber { get; set; }
        public string? CostListDescription { get; set; }
        public DateTime? CostListDate { get; set; }
        public int? CostListType { get; set; }
        public int? PaymentType { get; set; }
        public long? ContractId { get; set; }
        public long? WorkshopId { get; set; }
        public long? employerId { set; get; }
        public List<CostListDTO>? CostLists { get; set; }
        public CostListOrderBy? OrderBy { get; set; }
        public FilterCostListDTO SetPaging(BasePaging paging)
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

        public FilterCostListDTO SetCostList(List<CostListDTO> CostLists)
        {
            this.CostLists = CostLists;
            return this;
        }
    }




    public enum CostListOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
