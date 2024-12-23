
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Kol
{
    public class FilterKolDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? AccKolCode { get; set; }
        public int? KolCodeStart { get; set; }
        public int? KolCodeEnd { get; set; }
        public List<KolDTO>? Kols { get; set; }
        public List<long>? AccGroups { get; set; }
        public KolOrderBy? OrderBy { get; set; }
        public FilterKolDTO SetPaging(BasePaging paging)
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

        public FilterKolDTO SetKol(List<KolDTO> Kols)
        {
            this.Kols = Kols;
            return this;
        }

    }
    public enum KolOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
