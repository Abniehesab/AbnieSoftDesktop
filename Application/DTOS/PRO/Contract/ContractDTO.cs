using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.PRO.Contract
{
    public class ContractDTO
    {
        public long Id { get; set; }
        public long BusinessId { get; set; }      
        public DateTime? ContractDate { get; set; }
        public int? ContractCode { get; set; }
        public int? ContractNumber { get; set; }
        public int? ContractType { get; set; }
        public long? MoeinId { set; get; }
        public string? MoeinTitle { get; set; }
        public string? ContractTitle { get; set; }
        public string? ContractLocation { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? ContractPeriod { get; set; }
        public decimal? ContractPrice { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string? ContractAgreements { get; set; }
        public decimal? CostEstimation { get; set; }
        public float? PercentageOfChanges { get; set; }
        public long? ContractorId { set; get; }
        public string? ContractorTitle { set; get; }
        public long? TenderId { get; set; }
        public string? TenderTitle { get; set; }

        public int RowNumber { get; set; }
    }
}
