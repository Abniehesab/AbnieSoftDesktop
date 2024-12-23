
using Application.DTOS.ACC.Tazili;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tafzili2
{
    public class FilterTafzili2DTO:BasePaging
    {
        public long BusinessId { get; set; }
        public long? TafziliId { get; set; }
        public long? moeinId { get; set; }
        public string? Title { get; set; }
        public int? Tafzili2Code { get; set; }
        public DateTime? AccDocumentRowDateStart { get; set; }
        public DateTime? AccDocumentRowDateEnd { get; set; }
        public bool? Balance { get; set; }
        public bool? BothBalance { get; set; }
        public List<Tafzili2DTO>? Tafzili2s { get; set; }
        public Tafzili2OrderBy? OrderBy { get; set; }
        public FilterTafzili2DTO SetPaging(BasePaging paging)
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
        public FilterTafzili2DTO SetTafzili2(List<Tafzili2DTO> Tafzili2s)
        {
            this.Tafzili2s = Tafzili2s;
            return this;
        }
    }
    public enum Tafzili2OrderBy
    {
        CodeAsc,
        CodeDec
    }
}
