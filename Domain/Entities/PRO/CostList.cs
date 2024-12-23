using Domain.Entities.ACC;
using Domain.Entities.Common;
using Domain.Entities.FIN;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PRO
{
    public class CostList:BaseEntity
    {
        #region constructor
        [Display(Name = " تاریخ صورت هزینه")]
        public DateTime? CostListDate { get; set; }

        [Display(Name = "شماره صورت هزینه")]
        public int? CostListNumber { get; set; }


        [Display(Name = "نوع صورت هزینه")]
        public int? CostListType { get; set; }

     
        [Display(Name = "نوع پرداخت")]
        public int? PaymentType { get; set; }


        [Display(Name = "سرجمع صورت هزینه")]
        [Column(TypeName = "decimal(19,4)")] // تعیین نوع داده اعشاری به همراه تعداد ارقام قبل و بعد از اعشار
        public decimal? TotallyCostList { get; set; }

        public string? CostListDescription { get; set; }

        public bool IsUpdate { get; set; }

        #endregion
        #region relation

        public long? FK_AccDocumentRowLastModifierId { get; set; }

        [Display(Name = "کسب و کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //public Business Business { get; set; }
        public long? BusinessId { set; get; }


        [Display(Name = "سند حسابداری")]
        //[ForeignKey("AccDocument")]
        //public AccDocument AccDocument { get; set; }
        public long? AccDocumentId { set; get; }
        //public AccDocument AccDocument { get; set; }
      


        [Display(Name = "قرارداد")]
        //[ForeignKey("Contract")]
        public Contract Contract { get; set; }
        public long? ContractId { get; set; }
        


        [Display(Name = "تنخواه")]
        //[ForeignKey("InstantPayment")]
        public Tafzili InstantPayment { get; set; }
        public long? InstantPaymentId { get; set; }
        public long? MoeinForInstantPaymentId { get; set; }

        [Display(Name = "انبار")]
        //[ForeignKey("InstantPayment")]
        public Store Store { get; set; }
        public long? StoreId { get; set; }
        



        [Display(Name = "کارگاه")]
        //[ForeignKey("Workshop")]
        //public Workshop Workshop { get; set; }
        public long? WorkshopId { get; set; }


        [Display(Name = "کارفرما")]
        //[ForeignKey("Tafzili")]
        public Tafzili employer { get; set; }
        public long? employerId { set; get; }
        public long? MoeinForemployerId { get; set; }


        #endregion


    }
}
