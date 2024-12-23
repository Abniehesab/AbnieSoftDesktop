using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.AccDocmentDetails
{
    public class AccDocumentRowDataDTO
    {
        public DateTime AccDocumentRowDate { get; set; }
        public int AccDocumentRowNumber { get; set; }
        public int AccDocumentNumber { get; set; }
        public int Inflection { get; set; }
        public decimal DebtorValue { get; set; }
        public decimal CreditorValue { get; set; }
        public decimal TotalDebtorValue { get; set; }
        public decimal TotalCreditorValue { get; set; }
        public decimal Finalbalance { get; set; }
        public int NatureFinalBalance { get; set; }

    }
}
