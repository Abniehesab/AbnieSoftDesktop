using Application.DTOS.ACC.Kol;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.TafziliGroup
{
    public class FilterTafziliGroupDTO:BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? TafziliGroupCode { get; set; }
        public int? TafziliGroupCodeStart { get; set; }
        public int? TafziliGroupCodeEnd { get; set; }
        public List<TafziliGroupDTO>? TafziliGroups { get; set; }
        public List<long>? Moeins { get; set; }
        public List<long>? TafziliTypes { get; set; }
        public TafziliGroupOrderBy? OrderBy { get; set; }
        public FilterTafziliGroupDTO SetPaging(BasePaging paging)
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
        public FilterTafziliGroupDTO SetTafziliGroup(List<TafziliGroupDTO> TafziliGroups)
        {
            this.TafziliGroups = TafziliGroups;
            return this;
        }
    }
    public enum TafziliGroupOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
