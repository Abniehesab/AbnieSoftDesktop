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
    public class CostListDetails:BaseEntity
    {
        #region constructor
        [Display(Name = "ردیف لیست هزینه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? CostListRowNumber { get; set; }

        [Display(Name = " تاریخ ردیف لیست هزینه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public DateTime? CostListRowDate { get; set; }

        [Display(Name = "شماره لیست هزینه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? CostListNumber { get; set; }

        [Display(Name = "شماره رسید بانکی")]
        public string? ReceiptNumber { get; set; }

        [Display(Name = "مقدار کالا")]
        public float? AmountMaterial { get; set; }

        [Display(Name = " قیمت کالا")]
        public decimal? PriceMaterial { get; set; }

        [Display(Name = "شرح ردیف لیست هزینه")]
        public string CostListRowDescription { get; set; }

        [Display(Name = " قیمت اولیه هر سطر لیست هزینه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? CostListRowInitialValue { get; set; }

        [Display(Name = " قیمت نهایی هر سطر لیست هزینه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? CostListRowfinalValue { get; set; }

        public bool IsUpdate { get; set; }
        #endregion

        #region relation

        public long? FK_AccDocumentRowLastModifierId { get; set; }

        [Display(Name = "کسب و کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //public Business Business { get; set; }
        public long? BusinessId { set; get; }

        public CostList CostList { get; set; }
        public long? CostListId { set; get; }

        //public AccDocument AccDocument { get; set; }
        //public AccDocument AccDocument { get; set; }
        public long? AccDocumentId { set; get; }


        //public Kol Kol { get; set; }
        public long? KolId { get; set; }

        public Moein Moein { get; set; }
        public long? MoeinId { get; set; }

        public Tafzili Tafzili { get; set; }
        public long? TafziliId { get; set; }

        public Tafzili2 Tafzili2 { get; set; }
        public long? Tafzili2Id { get; set; }

        public Tafzili3 Tafzili3 { get; set; }
        public long? Tafzili3Id { get; set; }

        public Tafzili Supplier { get; set; }
        public long? SupplierId { get; set; }
        public long? MoeinForSupplierId { get; set; }

        public Tafzili Bank { get; set; }
        public long? BankId { get; set; }
        public long? MoeinForBankId { get; set; }


        [Display(Name = "طرف ضریب")]
        public Tafzili SideFactor { get; set; }
        public long? SideFactorId { get; set; }
        public long? MoeinForSideFactorId { get; set; }


        [Display(Name = "کد گروه کالا")]
        //[ForeignKey("MaterialGroup")]
        public MaterialGroup MaterialGroup { get; set; }
        public long? MaterialGroupId { get; set; }
        


        [Display(Name = "کد کالا")]
        //[ForeignKey("Material")]
        public Material Material { get; set; }
        public long? MaterialId { get; set; }
  

        [Display(Name = "کد واحد کالا")]
        //[ForeignKey("MaterialUnit")]
        public MaterialUnit MaterialUnit { get; set; }
        public long? MaterialUnitId { get; set; }
      


        [Display(Name = "کد ضریب ")]
        //[ForeignKey("ProjectStatusFactor")]
        //public ProjectStatusFactor ProjectStatusFactor { get; set; }
        public long? CostListFactorId { get; set; }
        
      
        


        #endregion
    }
}
