using Application.DTOS.ACC.Kol;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tazili
{
    public class FiltercostListDetailsDTO:BasePaging
    {
        public long BusinessId { get; set; }
        public long? MoeinId { get; set; }
        public long? TafziliId { get; set; }
        public string? Title { get; set; }
        public int? TafziliCode { get; set; }
        public int? AccTafziliCode { get; set; }
        public int? TafziliStart { get; set; }
        public int? TafziliEnd { get; set; }
        public int? TafziliType { get; set; }
        public int? NatureFinalBalance { get; set; }
        public decimal? FinalBalanceStart { get; set; }
        public decimal? FinalBalanceEnd { get; set; }

        public DateTime? AccDocumentRowDateStart { get; set; }
        public DateTime? AccDocumentRowDateEnd { get; set; }
        public bool? Balance { get; set; }
        public bool? BothBalance { get; set; }        
        public bool? Tafzili2 { get; set; }
        public bool? Tafzili3 { get; set; }

        public List<TafziliDTO>? Tafzilis { get; set; }
        public List<long>? TafziliGroups { get; set; }
        public TaziliOrderBy? OrderBy { get; set; }

        public FiltercostListDetailsDTO SetPaging(BasePaging paging)
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
        public FiltercostListDetailsDTO SetTazilis(List<TafziliDTO> Tafzilis)
        {
            this.Tafzilis = Tafzilis;
            return this;
        }
    }

    public enum TaziliOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
