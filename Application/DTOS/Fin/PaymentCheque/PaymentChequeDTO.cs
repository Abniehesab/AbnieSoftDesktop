using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Fin.PaymentCheque
{
    public class PaymentChequeDTO
    {
        public long Id { get; set; }
        public long? BusinessId { set; get; }
        public string? PaymentChequeNumber { get; set; }
        public string? PaymentChequeSayyadiNumber { get; set; }
        public long? BankId { get; set; }
        public string? BankTitle { get; set; }
        public long? ReciverId { get; set; }
        public string? ReciverTitle { get; set; }
        public long? ContractId { get; set; }
        public string? ContractTitle { get; set; }
        public DateTime? PaymentChequeDate { get; set; }
        public int? PaymentChequeLastState { get; set; }
        public bool? IsFirstPeriod { get; set; }
        public bool? IsGuarantee { get; set; }
        public decimal? PaymentChequeValue { get; set; }
        public string? PaymentChequeDescription { get; set; }
        public long? AccDocumentId { set; get; }

        public int RowNumber { get; set; }
    }
}
