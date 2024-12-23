using Application.DTOS.ACC.OtherTafzili;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.OtherTafzili
{
    public class FilterOtherTafziliDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? OtherTafziliCode { get; set; }
        public int? OtherTafziliCodeStart { get; set; }
        public int? OtherTafziliCodeEnd { get; set; }
        public long TafziliId { get; set; }
        public List<OtherTafziliDTO>? OtherTafzilis { get; set; }
        public List<long>? TafziliGroups { get; set; }
        public OtherTafziliOrderBy? OrderBy { get; set; }
        public FilterOtherTafziliDTO SetPaging(BasePaging paging)
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
        public FilterOtherTafziliDTO SetOtherTafzili(List<OtherTafziliDTO> OtherTafzilis)
        {
            this.OtherTafzilis = OtherTafzilis;
            return this;
        }
    }
    public enum OtherTafziliOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
