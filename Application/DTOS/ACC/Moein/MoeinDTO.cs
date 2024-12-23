using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Moein
{
    public class MoeinDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }

        public int MoeinCode { get; set; }
        public int AccMoeinCode { get; set; }
        public string MoeinName { get; set; }
        public long AccNatureId { set; get; }
        public string AccNatureName { set; get; }
        public long KolId { set; get; }
        public string KolName { set; get;}
        public int KolCode { set; get;}
        public decimal? FirstTotalDebtorValue { get; set; }
        public decimal? FirstTotalCreditorValue { get; set; }
        public decimal? TotalDebtorValue { get; set; }
        public decimal? TotalCreditorValue { get; set; }
        public int? NatureFinalBalance { get; set; }
        public decimal? Finalbalance { get; set; }


    }
}
