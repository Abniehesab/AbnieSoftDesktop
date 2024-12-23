using Domain.Entities.ACC;

using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.PRO
{
    public class Contract:BaseEntity
    {
        #region constructor

        [Display(Name = " تاریخ قرارداد")]
        public DateTime? ContractDate { get; set; }

        [Display(Name = "کد  قرارداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? ContractCode { get; set; }


        [Display(Name = "شماره قرارداد")]
        public int? ContractNumber { get; set; }


        [Display(Name = "نوع قرارداد")]
        public int? ContractType { get; set; }

        [Display(Name = "عنوان قرارداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? ContractTitle { get; set; }


        [Display(Name = "محل قرارداد")]
        public string? ContractLocation { get; set; }

        [Display(Name = " تاریخ شروع قرارداد")]
        public DateTime? ContractStartDate { get; set; }

        [Display(Name = " تاریخ پایان قرارداد")]
        public DateTime? ContractEndDate { get; set; }

        [Display(Name = "مدت قرارداد")]
        public int? ContractPeriod { get; set; }


        [Display(Name = "قیمت قرارداد")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? ContractPrice { get; set; }


        [Display(Name = " تاریخ فسخ قرارداد")]
        public DateTime? TerminationDate { get; set; }


        [Display(Name = "توافقات قرارداد")]
        public string? ContractAgreements { get; set; }



        [Display(Name = "برآورد قیمت")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? CostEstimation { get; set; }

        [Display(Name = "نرخ ضریب تغییرات قرارداد")]
        public float? PercentageOfChanges { get; set; }



        //[Display(Name = "تاریخچه قرارداد")]
        //public string? ContractHistory { get; set; }




        #endregion

        #region relation

        [Display(Name = "کد مناقصه")]
        public long? TenderId { get; set; }

        [Display(Name = "طرف مقابل")]
        public long? ContractorId { set; get; }

        [Display(Name = "کد معین پروژه")]
      
        public long? MoeinId { set; get; }
        public Moein Moein { get; set; }

        [Display(Name = "کسب و کار")]
        //public Business Business { get; set; }
        public long? BusinessId { set; get; }


        #endregion
    }
}
