using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tazili
{
    public class TafziliDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public long TafziliGroupId { get; set; }
        public String TafziliGroupName { get; set; }
        public int? TafziliCode { get; set; }
        public int AccTafziliCode { get; set; }
        public string? TafziliName { get; set; }
        public int? TafziliType { get; set; }
        public string TafziliTypeName { get; set; }
        public long? TafziliRef { get; set; }
        public bool Tafzili2 { get; set; }
        public bool Tafzili3 { get; set; }

        public decimal? FirstTotalDebtorValue { get; set; }
        public decimal? FirstTotalCreditorValue { get; set; }
        public decimal? TotalDebtorValue { get; set; }
        public decimal? TotalCreditorValue { get; set; }
        public int? NatureFinalBalance { get; set; }
        public decimal? Finalbalance { get; set; }

        public decimal? FirstTotalDebtorValueTafzili2 { get; set; }
        public decimal? FirstTotalCreditorValueTafzili2 { get; set; }
        public decimal? TotalDebtorValueTafzili2 { get; set; }
        public decimal? TotalCreditorValueTafzili2 { get; set; }
        public int? NatureFinalBalanceTafzili2 { get; set; }
        public decimal? FinalbalanceTafzili2 { get; set; }


        public decimal? FirstTotalDebtorValueTafzili3 { get; set; }
        public decimal? FirstTotalCreditorValueTafzili3 { get; set; }
        public decimal? TotalDebtorValueTafzili3 { get; set; }
        public decimal? TotalCreditorValueTafzili3 { get; set; }
        public int? NatureFinalBalanceTafzili3 { get; set; }
        public decimal? FinalbalanceTafzili3 { get; set; }

        public int RowNumber { get; set; }
    }
}
