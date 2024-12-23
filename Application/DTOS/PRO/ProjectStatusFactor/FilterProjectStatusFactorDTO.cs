using Application.DTOS.PRO.Material;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.ProjectStatusFactor
{
    public class FilterProjectStatusFactorDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public int? ProjectStatusFactorCode { get; set; }
        public string? ProjectStatusFactorTitle { get; set; }
        public int? ProjectStatusFactorType { get; set; }
        public int? ProjectStatusFactorUser { get; set; }
        public long? MoeinId { set; get; }
        public List<ProjectStatusFactorDTO>? ProjectStatusFactors { get; set; }
        public ProjectStatusFactorOrderBy? OrderBy { get; set; }

        public FilterProjectStatusFactorDTO SetPaging(BasePaging paging)
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

        public FilterProjectStatusFactorDTO SetProjectStatusFactor(List<ProjectStatusFactorDTO> projectStatusFactors)
        {
            this.ProjectStatusFactors = projectStatusFactors;
            return this;
        }

    }


    public enum ProjectStatusFactorOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
