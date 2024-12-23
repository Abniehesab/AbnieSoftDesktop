
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.Contract
{
    public class FilterContractDTO : BasePaging
    {
        public long BusinessId { get; set; }
        public string? Title { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? ContractCode { get; set; }
        public int? ContractNumber { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? ContractType { get; set; }
        public ContractOrderBy? OrderBy { get; set; }
        public List<ContractDTO>? Contracts { get; set; }

        public FilterContractDTO SetPaging(BasePaging paging)
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
        public FilterContractDTO SetContract(List<ContractDTO> contracts)
        {
            this.Contracts = contracts;
            return this;
        }

    }
    public enum ContractOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
