using Application.DTOS.PRO.MaterialGroup;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.MaterialGroup
{
    public class FilterMaterialGroupDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? MaterialGroupCode { get; set; }
        public List<MaterialGroupDTO>? MaterialGroups { get; set; }
        public MaterialGroupOrderBy? OrderBy { get; set; }
        public FilterMaterialGroupDTO SetPaging(BasePaging paging)
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

        public FilterMaterialGroupDTO SetMaterialGroup(List<MaterialGroupDTO> MaterialGroups)
        {
            this.MaterialGroups = MaterialGroups;
            return this;
        }
    }
    public enum MaterialGroupOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
