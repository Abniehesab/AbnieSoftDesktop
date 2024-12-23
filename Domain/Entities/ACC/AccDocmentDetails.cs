
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
    public class AccDocmentDetails : BaseEntity
    {
        #region constructor
        [Display(Name = "ردیف سند حسابداری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? AccDocumentRowNumber { get; set; }

        [Display(Name = " تاریخ ردیف سند حسابداری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public DateTime? AccDocumentRowDate { get; set; }

        [Display(Name = "شماره سند حسابداری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? AccDocumentNumber { get; set; }

        [Display(Name = "عطف سند حسابداری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? Inflection { get; set; }


        [Display(Name = "نوع سند حسابداری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? AccDocumentType { get; set; }

        [Display(Name = "شرح ردیف سند حسابداری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(5000, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string AccDocumentRowDescription { get; set; }

        [Display(Name = " بدهکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? DebtorValue { get; set; }

        [Display(Name = " بستانکار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? CreditorValue { get; set; }
        public bool IsUpdate { get; set; }

        #endregion

        #region relation
        //public User User { get; set; }
        public long? FK_AccDocumentRowLastModifierId { get; set; }

        [Display(Name = "کسب و کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //public Business Business { get; set; }
        public long? BusinessId { set; get; }

        //public AccDocument AccDocument { get; set; }
        public long? AccDocumentId { set; get; }

        public Kol Kol { get; set; }
        public long? KolId { get; set; }

        public Moein Moein { get; set; }
        public long? MoeinId { get; set; }

        public Tafzili Tafzili { get; set; }
        public long? TafziliId { get; set; }

        public Tafzili2 Tafzili2 { get; set; }

        public long? Tafzili2Id { get; set; }

        public Tafzili3 Tafzili3 { get; set; }
        public long? Tafzili3Id { get; set; }


        #endregion
    }

}
