using Application.DTOS.ACC.Moein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.TafziliGroup
{
    public class TafziliGroupDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }
        public int? TafziliGroupCode { get; set; }
        public string TafziliGroupName { get; set; }
        //public List<long> MoeinId { get; set; }
        //public List<string> MoeinName { get; set; }
        //public List<int?> MoeinCode { get; set; }
        //public List<int?> accMoeinCode { get; set; }
        public List<MoeinDTO> Moeins { get; set; }
        public long TafziliTypeId { get; set; }
        public string TafziliTypeName { get; set; }

        public decimal? FirstTotalDebtorValue { get; set; }
        public decimal? FirstTotalCreditorValue { get; set; }
        public decimal? TotalDebtorValue { get; set; }
        public decimal? TotalCreditorValue { get; set; }
        public int? NatureFinalBalance { get; set; }
        public decimal? Finalbalance { get; set; }




    }
}
