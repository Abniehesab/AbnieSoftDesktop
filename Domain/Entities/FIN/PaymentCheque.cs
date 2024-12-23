using Domain.Entities.ACC;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIN
{
    public class PaymentCheque:BaseEntity
    {
        #region constructor

        [Display(Name = "شماره پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? PaymentNumber { get; set; }

        [Display(Name = "ردیف لیست چک پرداختی در پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? PaymentChequeRowNumber { get; set; }

        [Display(Name = "شماره چک پرداختی")]
        public string? PaymentChequeNumber { get; set; }

        [Display(Name = "شماره صیادی چک پرداختی")]
        public string? PaymentChequeSayyadiNumber { get; set; }

        [Display(Name = " تاریخ چک پرداختی")]
        public DateTime? PaymentChequeDate { get; set; }

        [Display(Name = " تاریخ پرداخت")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "جزئیات چک پرداختی")]
        public string? PaymentChequeDescription { get; set; }

        
        [Display(Name = "ارزش چک پرداختی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? PaymentChequeValue { get; set; }

        [Display(Name = "چک اول دوره")]
        public bool? IsFirstPeriod { get; set; }

        [Display(Name = "چک ضمانتی")]
        public bool? IsGuarantee { get; set; }


        public bool IsUpdate { get; set; }

        [Display(Name = "آخرین وضعیت چک پرداختی")]
        public int? PaymentChequeLastState { get; set; }

        #endregion

        #region relation


        [Display(Name = "کسب و کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      
        public long? BusinessId { set; get; }

        [Display(Name = "پرداخت")]
        //public Payment Payment { get; set; }
        public long? PaymentId { set; get; }

        [Display(Name = "بانک صادرکننده چک")]
        public Tafzili Bank { get; set; }
        public long? BankId { get; set; }

        [Display(Name = "معین بانک صادرکننده چک")]
        public Moein MoeinForBank { get; set; }
        public long? MoeinForBankId { get; set; }


        [Display(Name = "دریافت کننده")]
        public Tafzili Reciver { get; set; }
        public long? ReciverId { get; set; }

        [Display(Name = "معین دریافت کننده")]
        public Moein MoeinForReciver { get; set; }
        public long? MoeinForReciverId { get; set; }

        [Display(Name = "قرارداد")]
        public Domain.Entities.PRO.Contract Contract { get; set; }
        public long? ContractId { get; set; }

        [Display(Name = "معین حساب چک پرداختی")]
        public Moein MoeinForPaymentCheque { get; set; }
        public long? MoeinForPaymentChequeId { get; set; }

        [Display(Name = "سند حسابداری")]
        //public AccDocument AccDocument { get; set; }
        public long? AccDocumentId { set; get; }
        #endregion
    }
}
