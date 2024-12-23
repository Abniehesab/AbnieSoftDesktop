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
    public class Tafzili3 : BaseEntity
    {
        #region constructor

        [Display(Name = "کد حساب تفضیلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? Tafzili3Code { get; set; }

        [Display(Name = "نام حساب تفضیلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? Tafzili3Name { get; set; }


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

        #endregion

        #region relation
        [Display(Name = "ایجاد کننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [ForeignKey("Business")]
        public long BusinessId { set; get; }

        #endregion

    }
}
