
using Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.AccDocmentDetails
{
    public class FilterAccDocmentDetailsDTO:BasePaging
    {
        public long BusinessId { get; set; }
        public int? AccDocumentNumber { get; set; }
        public string? AccDocumentRowDescription { get; set; }
        public int? Inflection { get; set; }
        public DateTime? AccDocumentRowDate { get; set; }
        public DateTime? AccDocumentRowDateStart { get; set; }
        public DateTime? AccDocumentRowDateEnd { get; set; }
        public decimal? DebtorValue { get; set; }
        public decimal? CreditorValue { get; set; }
        public long? KolId { get; set; }
        public bool KolAccountingBook { get; set; }
        public long? MoeinId { get; set; }
        public long? TafziliId { get; set; }
        public long? Tafzili2Id { get; set; }
        public long? Tafzili3Id { get; set; }

        public List<AccDocmentDetailsDTO>? Invoices { get; set; }
        public AccDocumentDetailsOrderBy? OrderBy { get; set; }

        public FilterAccDocmentDetailsDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            return this;
        }
        public FilterAccDocmentDetailsDTO SetAccDocumentDetails(List<AccDocmentDetailsDTO> AccDocmentDetails )
        {
            this.Invoices = AccDocmentDetails;
            return this;
        }

    }
    public enum AccDocumentDetailsOrderBy
    {
        CodeAsc,
        CodeDec
    }
}
