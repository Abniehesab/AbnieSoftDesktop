using Application.DTOS.Fin.ReceiveCheque;
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Fin.ReceiveCheque
{
    public class FilterReceiveChequeDTO:BasePaging
    {
        public long? BusinessId { set; get; }
        public int? ReceiveChequeLastState { get; set; }
        public bool? IsGuarantee { get; set; }
        public bool? IsFirstPeriod { get; set; }
        public string? ReceiveChequeNumber { get; set; }
        public string? ReceiveChequeSayyadiNumber { get; set; }

        public DateTime? ReceiveChequeDateStart { get; set; }
        public DateTime? ReceiveChequeDateEnd { get; set; }

        public long? BankId { get; set; }
        public long? PayerId { get; set; }
        public long? ContractId { get; set; }
        public string? ReceiveChequeDescription { get; set; }
        public decimal? ReceiveChequeValue { get; set; }
        public List<ReceiveChequeDTO>? ReceiveCheques { get; set; }
        public ReceiveChequesOrderBy? OrderBy { get; set; }
        public FilterReceiveChequeDTO SetPaging(BasePaging paging)
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

        public FilterReceiveChequeDTO SetReceiveCheques(List<ReceiveChequeDTO> ReceiveCheques)
        {
            this.ReceiveCheques = ReceiveCheques;
            return this;
        }

    }
    public enum ReceiveChequesOrderBy
    {
        CodeAsc,
        CodeDec
    }

}
