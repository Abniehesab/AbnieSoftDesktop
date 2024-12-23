using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Tafzili2
{
    public class Tafzili2DTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? Tafzili2Code { get; set; }  
        public string? Tafzili2Name { get; set; }
        public decimal? FirstTotalDebtorValue { get; set; }
        public decimal? FirstTotalCreditorValue{ get; set; }
        public decimal? TotalDebtorValue { get; set; }
        public decimal? TotalCreditorValue{ get; set; }
        public int? NatureFinalBalance { get; set; }
        public decimal? Finalbalance{ get; set; }
    }
}
