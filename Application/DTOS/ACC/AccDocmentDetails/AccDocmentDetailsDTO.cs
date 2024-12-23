using Domain.Entities.ACC;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.AccDocmentDetails
{
    public class AccDocmentDetailsDTO
    {
        public long Id { get; set; }
        public long? BusinessId { set; get; }
        public int? AccDocumentRowNumber { get; set; }
        public DateTime? AccDocumentRowDate { get; set; }
        public int? Inflection { get; set; }      
        public string AccDocumentRowDescription { get; set; }      
        public decimal? DebtorValue { get; set; }
        public decimal? CreditorValue { get; set; }
        public long? FK_AccDocumentRowLastModifierId { get; set; }
        public int? AccDocumentNumber { get; set; }
        public long? AccDocumentId { set; get; }
        public string? KolId { get; set; }
        public string? KolName { get; set; }
        public int? AccKolCode { get; set; }
        public long? MoeinId { get; set; }
        public string? MoeinName { get; set; }
        public int? AccMoeinCode { get; set; }
        public long? TafziliId { get; set; }
        public string? TafziliName { get; set; }
        public int? AccTafziliCode { get; set; }
        public long? Tafzili2Id { get; set; }
        public string? Tafzili2Name { get; set; }
        public int? AccTafzili2Code { get; set; }
        public long? Tafzili3Id { get; set; }
        public string? Tafzili3Name { get; set; }
        public int? AccTafzili3Code { get; set; }
        public decimal? TotalDebtorValue { get; set; }
        public decimal? TotalCreditorValue { get; set; }
        public decimal? Finalbalance { get; set; }
        public int? NatureFinalBalance { get; set; }

        public int RowNumber { get; set; }

    }
}
