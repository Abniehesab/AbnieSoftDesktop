using Domain.Entities.ACC;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PRO
{
    public class Tender:BaseEntity
    {
        #region constructor
        [Display(Name = "کد مناقصه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? TenderCode { get; set; }

        [Display(Name = "عنوان مناقصه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? TenderTitle { get; set; }

        [Display(Name = "شماره مناقصه")]
        public int? TenderNumber { get; set; }

        [Display(Name = " تاریخ مناقصه")]
        public DateTime? TenderDate { get; set; }

        [Display(Name = "نوع مناقصه")]

        public int? TenderType { set; get; }


        [Display(Name = " تاریخ شروع مناقصه")]
        public DateTime? TenderStartDate { get; set; }

        [Display(Name = " تاریخ پایان مناقصه")]
        public DateTime? TenderEndDate { get; set; }

        [Display(Name = "مدت مناقصه")]
        public int? TenderPeriod { get; set; }


        [Display(Name = "مشخصات و مقادیر کار")]
        public string? AmountWork { get; set; }

        [Display(Name = "محل اجرای کار")]
        public string? PlaceOfWork { get; set; }

        [Display(Name = "میزان مبلغ سپرده ضمانتنامه شرکت در مناقصه")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? WarrancyAmount { get; set; }


        [Display(Name = "پیش پرداخت")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? PrePayment { get; set; }


        [Display(Name = "محل توزیع و فروش اسناد مناقصه")]
        public string? PlaceOfWorksaleOfTenderDocuments { get; set; }

        [Display(Name = " تاریخ خواندن پیشنهادها")]
        public DateTime? ReadingTenderOffersDate { get; set; }

        [Display(Name = "مکان خواندن پیشنهادها")]
        public string? ReadingTenderOffersPlace { get; set; }

        [Display(Name = "قیمت پیشنهادی")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? ProposedPrice { get; set; }

        #endregion

        #region relation
    
      


        [Display(Name = "مناقصه گذار")]
        [ForeignKey("Tafzili")]
        public long? TenderOwnerId { set; get; }
        public Tafzili Tafzili { get; set; }
      


        [Display(Name = "کسب و کار")]
        //public Business Business { get; set; }
        public long? BusinessId { set; get; }


        #endregion
    }
}
