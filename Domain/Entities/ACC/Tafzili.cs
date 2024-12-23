using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.ACC
{
    public class Tafzili : BaseEntity
    {
        #region constructor

        [Display(Name = "کد حساب تفضیلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? TafziliCode { get; set; }

        [Display(Name = "نام حساب تفضیلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? TafziliName { get; set; }

        [Display(Name = "کد حساب کل در حسابداری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int AccTafziliCode { get; set; }

        [Display(Name = "نوع حساب تفضیلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int TafziliType { get; set; }

        [Display(Name = "رجوع حساب تفضیلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public long? TafziliRef { get; set; }

        [Display(Name = "کلید مشترک")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? SharedKey { get; set; }

        [Display(Name = "  جمع ستون بدهکار اول دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FirstTotalDebtorValue { get; set; }

        [Display(Name = " جمع ستون بستانکار اول دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FirstTotalCreditorValue { get; set; }

        [Display(Name = " جمع ستون بدهکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? TotalDebtorValue { get; set; }

        [Display(Name = " جمع ستون بستانکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? TotalCreditorValue { get; set; }

        [Display(Name = "ماهیت مانده نهایی تفضیلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? NatureFinalBalance { get; set; }

        [Display(Name = " مانده نهایی تفضیلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? Finalbalance { get; set; }

        [Display(Name = "ماهیت مانده نهایی تفضیلی2")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? NatureFinalBalanceTafzili2 { get; set; }

        [Display(Name = "  مانده نهایی تفضیلی2")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FinalbalanceTafzili2 { get; set; }

        [Display(Name = "ماهیت مانده نهایی تفضیلی3")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? NatureFinalBalanceTafzili3 { get; set; }

        [Display(Name = "  مانده نهایی تفضیلی3")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FinalbalanceTafzili3 { get; set; }

        #endregion

        #region relation
        [Display(Name = "نام گروه حساب تفضیلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [ForeignKey("TafziliGroup")]
        public long? TafziliGroupId { get; set; }
        //public  TafziliGroup TafziliGroup { get; set; }
       

        public long BusinessId { set; get; }

        [Display(Name = "دسترسی در سطح تفضیلی 2 سند ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool Tafzili2 { get; set; }

        [Display(Name = "  جمع ستون بدهکار اول دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FirstTotalDebtorValueTafzili2 { get; set; }

        [Display(Name = " جمع ستون بستانکار اول دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FirstTotalCreditorValueTafzili2 { get; set; }

        [Display(Name = " جمع ستون بدهکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? TotalDebtorValueTafzili2 { get; set; }

        [Display(Name = " جمع ستون بستانکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? TotalCreditorValueTafzili2 { get; set; }


        [Display(Name = "دسترسی در سطح تفضیلی 3 سند ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool Tafzili3{ get; set; }

        [Display(Name = "  جمع ستون بدهکار اول دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FirstTotalDebtorValueTafzili3 { get; set; }

        [Display(Name = " جمع ستون بستانکار اول دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? FirstTotalCreditorValueTafzili3 { get; set; }


        [Display(Name = " جمع ستون بدهکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? TotalDebtorValueTafzili3 { get; set; }

        [Display(Name = " جمع ستون بستانکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? TotalCreditorValueTafzili3 { get; set; }

        #endregion


        //[ForeignKey("TafziliGroupId")]
        //public virtual TafziliGroup TafziliGroup { get; set; }
        //public int TafziliGroupId { get; set; }

        //public ICollection<Tafzili2> Tafziliis   { get; set; }

        //public Moein Moein { get; set; }
    }
}
