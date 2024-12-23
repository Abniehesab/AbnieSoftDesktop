using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.ACC.AccDocmentDetails
{
    public class RegisterAccDocmentDetailsDTO
    {
        public int? AccDocumentRowNumber { get; set; }
        public string AccDocumentRowDescription { get; set; }
        public long? KolId { get; set; }
        public long? MoeinId { get; set; }
        public long? TafziliId { get; set; }
        public long? Tafzili2Id { get; set; }
        public long? Tafzili3Id { get; set; }
        public decimal? DebtorValue { get; set; }
        public decimal? CreditorValue { get; set; }
    }
    public enum RegisterAccDocumentDetailsResult
    {
        Success,
        Unbalanced,  //اگر مجموع سطرهای بدهکار و بستانکار یکی نبودند
        OppositeNature,//اگر ماند ه حساب معین خلاف ماهیت باشد
        UnSuccess,
    }
}
