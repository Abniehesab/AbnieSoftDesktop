using Application.DTOS.ACC.Kol;
using Common.Utilities.Paging;
using Domain.Entities.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Moein
{
    public class FilterMoeinDTO:BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? AccMoeinCode { get; set; }
        public int? MoeinCodeStart { get; set; }
        public int? MoeinCodeEnd { get; set; }
        public List<MoeinDTO>? Moeins { get; set; }
        public List<long>? Kols { get; set; }
        public MoeinOrderBy? OrderBy { get; set; }
        public FilterMoeinDTO SetPaging(BasePaging paging)
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


        public FilterMoeinDTO SetMoein(List<MoeinDTO> Moeins)
        {
            this.Moeins = Moeins;
            return this;
        }

    }
    public enum MoeinOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
