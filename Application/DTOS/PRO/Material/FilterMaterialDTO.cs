using Application.DTOS.PRO.MaterialGroup;

using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.Material
{
    public class FilterMaterialDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? MaterialCode { get; set; }
        public string? MaterialGroupTitle { get; set; }
        public string? MaterialDescription { get; set; }
        public long? MaterialGroupId { get; set; }
        public List<MaterialDTO>? Materials { get; set; }
        public MaterialGroupOrderBy? OrderBy { get; set; }

        public FilterMaterialDTO SetPaging(BasePaging paging)
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

        public FilterMaterialDTO SetMaterial(List<MaterialDTO> materials)
        {
            this.Materials = materials;
            return this;
        }

    }

    public enum MaterialOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
