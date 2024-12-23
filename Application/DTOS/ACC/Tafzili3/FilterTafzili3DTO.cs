using Application.DTOS.ACC.Tafzili3;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tafzili3
{
    public class FilterTafzili3DTO : BasePaging
    {
        public long BusinessId { get; set; }
        public long? TafziliId { get; set; }
        public long? Tafzili2Id { get; set; }
        public long? MoeinId { get; set; }
        public string? Title { get; set; }
        public int? Tafzili3Code { get; set; }
        public DateTime? AccDocumentRowDateStart { get; set; }
        public DateTime? AccDocumentRowDateEnd { get; set; }
        public bool? Balance { get; set; }
        public bool? BothBalance { get; set; }
        public List<Tafzili3DTO>? Tafzili3s { get; set; }
        public Tafzili3OrderBy? OrderBy { get; set; }
        public FilterTafzili3DTO SetPaging(BasePaging paging)
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
        public FilterTafzili3DTO SetTafzili3(List<Tafzili3DTO> Tafzili3s)
        {
            this.Tafzili3s = Tafzili3s;
            return this;
        }
    }
    public enum Tafzili3OrderBy
    {
        CodeAsc,
        CodeDec
    }
}
