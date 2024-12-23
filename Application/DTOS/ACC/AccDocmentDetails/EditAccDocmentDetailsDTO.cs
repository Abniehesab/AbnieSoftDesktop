using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.AccDocmentDetails
{
    public class EditAccDocmentDetailsDTO
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
        public long? KolId { get; set; }
        public long? MoeinId { get; set; }
        public long? TafziliId { get; set; }
        public long? Tafzili2Id { get; set; }
        public long? Tafzili3Id { get; set; }
    }
    public enum EditAccDocmentDetailsResult
    {
        Success,
        Unbalanced,  //اگر مجموع سطرهای بدهکار و بستانکار یکی نبودند
        OppositeNature,  //اگر ماند ه حساب معین خلاف ماهیت باشد
        UnSuccess,
    }
}
