using Domain.Entities.ACC;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIN
{
    public class ReceiveCheque:BaseEntity
    {
        #region constructor
        [Display(Name = "شماره دریافت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? ReceiveNumber { get; set; }

        [Display(Name = "ردیف لیست چک دریافتی در دریافت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? ReceiveChequeRowNumber { get; set; }

        [Display(Name = "شماره چک دریافتی")]
        public string? ReceiveChequeNumber { get; set; }

        [Display(Name = "شماره صیادی چک دریافتی")]
        public string? ReceiveChequeSayyadiNumber { get; set; }

        [Display(Name = " تاریخ چک دریافتی")]
        public DateTime? ReceiveChequeDate { get; set; }

        [Display(Name = " تاریخ دریافت")]
        public DateTime? ReceiveDate { get; set; }

        [Display(Name = " تاریخ خرج")]
        public DateTime? PaymentToDate { get; set; }

        [Display(Name = "جزئیات چک دریافتی")]
        public string? ReceiveChequeDescription { get; set; }


        [Display(Name = "ارزش چک دریافتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? ReceiveChequeValue { get; set; }

        [Display(Name = "چک اول دوره")]
        public bool? IsFirstPeriod { get; set; }

        [Display(Name = "چک ضمانتی")]
        public bool? IsGuarantee { get; set; }


        public bool IsUpdate { get; set; }

        [Display(Name = "آخرین وضعیت چک دریافتی")]
        public int? ReceiveChequeLastState { get; set; }

        [Display(Name = "آخرین وضعیت چک دریافتی")]
        public int? ReceiveChequeBeforeLastState { get; set; }

        #endregion

        #region relation

        [Display(Name = "کسب و کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? BusinessId { set; get; }


        [Display(Name = "دریافت")]
        //public Receive? Receive { get; set; }
        public long? ReceiveId { set; get; }

        [Display(Name = "پرداخت - خرج")]
        //public Payment? Payment { get; set; }
        public long? PaymentId { set; get; }

        [Display(Name = "بانک خواباندن به حساب چک دریافتی")]
        public Tafzili? Bank { get; set; }
        public long? BankId { get; set; }

        [Display(Name = " معین بانک خواباندن به حساب چک دریافتی")]
        public Moein? MoeinForBank { get; set; }
        public long? MoeinForBankId { get; set; }


        [Display(Name = "پرداخت کننده")]
        public Tafzili? Payer { get; set; }
        public long? PayerId { get; set; }

        [Display(Name = "معین پرداخت کننده")]
        public Moein? MoeinForPayer { get; set; }
        public long? MoeinForPayerId { get; set; }

        [Display(Name = "خرج به")]
        public Tafzili? PaymentTo { get; set; }
        public long? PaymentToId { get; set; }

        [Display(Name = "معین خرج به")]
        public Moein? MoeinForPaymentTo { get; set; }
        public long? MoeinForPaymentToId { get; set; }

        [Display(Name = "قرارداد")]
        public Domain.Entities.PRO.Contract? Contract { get; set; }
        public long? ContractId { get; set; }

        [Display(Name = "معین حساب چک دریافتی")]
        public Moein? MoeinForReceiveCheque { get; set; }
        public long? MoeinForReceiveChequeId { get; set; }

        [Display(Name = "سند حسابداری")]
        //public AccDocument AccDocument { get; set; }
        public long? AccDocumentId { set; get; }

        #endregion
    }
}
