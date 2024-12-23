using Application.DTOS.PRO.Material;
using Application.DTOS.PRO.MaterialGroup;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.Store
{
    public class FilterStoreDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? StoreCode { get; set; }
  
        public List<StoreDTO>? Stores { get; set; }
        public StoreOrderBy? OrderBy { get; set; }

        public FilterStoreDTO SetPaging(BasePaging paging)
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

        public FilterStoreDTO SetStore(List<StoreDTO> stores)
        {
            this.Stores = stores;
            return this;
        }

    }

    public enum StoreOrderBy
    {
        CodeAsc,
        CodeDec
    }

}
