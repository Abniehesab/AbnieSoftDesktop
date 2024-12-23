using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.Kol
{
    public class KolDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? KolCode { get; set; }
        public int? AccKolCode { get; set; }
        public string KolName { get; set; }
        public long AccGroupId { get; set; }
        public string AccGroupName { get; set; }
        public int? AccGroupCode { get; set; }

        public decimal? FirstTotalDebtorValue { get; set; }
        public decimal? FirstTotalCreditorValue { get; set; }
        public decimal? TotalDebtorValue { get; set; }
        public decimal? TotalCreditorValue { get; set; }
        public int? NatureFinalBalance { get; set; }
        public decimal? Finalbalance { get; set; }


    }
}
