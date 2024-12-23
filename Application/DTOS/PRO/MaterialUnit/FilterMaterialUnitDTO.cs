using Application.DTOS.PRO.MaterialUnit;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.MaterialUnit
{
    public class FilterMaterialUnitDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? MaterialUnitCode { get; set; }
        public List<MaterialUnitDTO>? MaterialUnits { get; set; }
        public MaterialUnitOrderBy? OrderBy { get; set; }
        public FilterMaterialUnitDTO SetPaging(BasePaging paging)
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

        public FilterMaterialUnitDTO SetMaterialUnit(List<MaterialUnitDTO> MaterialUnits)
        {
            this.MaterialUnits = MaterialUnits;
            return this;
        }
    }
    public enum MaterialUnitOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
