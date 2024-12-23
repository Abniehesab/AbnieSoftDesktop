using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tafzili3
{
    public class Tafzili3DTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? Tafzili3Code { get; set; }
        public string? Tafzili3Name { get; set; }
        public decimal? FirstTotalDebtorValue { get; set; }
        public decimal? FirstTotalCreditorValue { get; set; }
        public decimal? TotalDebtorValue { get; set; }
        public decimal? TotalCreditorValue { get; set; }
        public int? NatureFinalBalance { get; set; }
        public decimal? Finalbalance { get; set; }
    }
}
