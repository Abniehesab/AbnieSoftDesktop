using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Fin.ReceiveCheque
{
    public class ReceiveChequeDTO
    {
        public long Id { get; set; }
        public long? BusinessId { set; get; }
        public string? ReceiveChequeNumber { get; set; }
        public string? ReceiveChequeSayyadiNumber { get; set; }
        public long? BankId { get; set; }
        public string? BankTitle { get; set; }
        public long? PayerId { get; set; }
        public string? PayerTitle { get; set; }
        public long? ContractId { get; set; }
        public string? ContractTitle { get; set; }
        public DateTime? ReceiveChequeDate { get; set; }
        public int? ReceiveChequeLastState { get; set; }
        public bool? IsFirstPeriod { get; set; }
        public bool? IsGuarantee { get; set; }
        public decimal? ReceiveChequeValue { get; set; }
        public string? ReceiveChequeDescription { get; set; }
        public long? AccDocumentId { set; get; }

        public int RowNumber { get; set; }
    }
}
