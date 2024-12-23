
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Fin.PaymentCheque
{
    public class FilterPaymentChequeDTO:BasePaging
    {
    
        public long? BusinessId { set; get; }
        public int? PaymentChequeLastState { get; set; }
        public bool? IsGuarantee { get; set; }
        public bool? IsFirstPeriod { get; set; }
        public string? PaymentChequeNumber { get; set; }
        public string? PaymentChequeSayyadiNumber { get; set; }
  
        public DateTime? PaymentChequeDateStart { get; set; }
        public DateTime? PaymentChequeDateEnd { get; set; }

        public long? BankId { get; set; }
        public long? ReciverId { get; set; }
        public long? ContractId { get; set; }
        public string? PaymentChequeDescription { get; set; }
        public decimal? PaymentChequeValue { get; set; }
        public List<PaymentChequeDTO>? PaymentCheques{ get; set; }
        public PaymentChequesOrderBy? OrderBy { get; set; }
        public FilterPaymentChequeDTO SetPaging(BasePaging paging)
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

        public FilterPaymentChequeDTO SetPaymentCheques(List<PaymentChequeDTO> PaymentCheques)
        {
            this.PaymentCheques = PaymentCheques;
            return this;
        }

    }
    public enum PaymentChequesOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
