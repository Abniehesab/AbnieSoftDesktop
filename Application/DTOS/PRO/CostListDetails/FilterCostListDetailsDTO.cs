using Application.DTOS.ACC.AccDocmentDetails;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.CostListDetails
{
    public class FilterCostListDetailsDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public int? CostListNumber { get; set; }
        public string? CostListRowDescription { get; set; }
        public DateTime? CostListRowDate { get; set; }
        public DateTime? CostListRowDateStart { get; set; }
        public DateTime? CostListRowDateEnd { get; set; }
        public decimal? CostListRowfinalValue { get; set; }
        public long? MaterialGroupId { get; set; }
        public long? MaterialId { get; set; }
        public long? SupplierId { get; set; }
        public long? SideFactorId { get; set; }
        public long? BankId { get; set; }
        public string? ReceiptNumber { get; set; }

        public long? KolId { get; set; }
        public long? MoeinId { get; set; }
        public long? TafziliId { get; set; }
        public long? Tafzili2Id { get; set; }
        public long? Tafzili3Id { get; set; }
        public List<CostListDetailsDTO>? CostListDetails { get; set; }
        public CostListDetailsOrderBy? OrderBy { get; set; }
        public FilterCostListDetailsDTO SetPaging(BasePaging paging)
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

        public FilterCostListDetailsDTO SetCostListDetails(List<CostListDetailsDTO> CostListDetails)
        {
            this.CostListDetails = CostListDetails;
            return this;
        }

    }
    public enum CostListDetailsOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
