using Domain.Entities.ACC;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PRO
{
    public class MaterialCirculation : BaseEntity
    {
        #region constructor


        [Display(Name = "نوع گردش کالا")]
        public int? MaterialCirculationType { get; set; } 


        [Display(Name = "ردیف گردش کالا ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? MaterialCirculationRowNumber { get; set; }

        [Display(Name = " تاریخ گردش کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public DateTime? MaterialCirculationRowDate { get; set; }

        [Display(Name = "شماره عملیات گردش کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? MaterialCirculationOperationNumber { get; set; }

        [Display(Name = "مقدار کالای ورودی")]
        public float? AmountMaterialEntered { get; set; }

        [Display(Name = " قیمت کالای ورودی")]
        public decimal? PriceMaterialEntered { get; set; }

        [Display(Name = "مقدار کالای خروجی")]
        public float? AmountMaterialOutput { get; set; }

        [Display(Name = " قیمت کالای خروجی")]
        public decimal? PriceMaterialOutput { get; set; }

        [Display(Name = "شرح ردیف گردش کالا")]
        public string MaterialCirculationRowDescription { get; set; }

        public bool IsUpdate { get; set; }
        #endregion

        #region relation

        public long? FK_MaterialCirculationRowLastModifierId { get; set; }

        [Display(Name = "کسب و کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //public Business Business { get; set; }
        public long? BusinessId { set; get; }

        [Display(Name = "قرارداد")]
        //[ForeignKey("Contract")]
        public Contract Contract { get; set; }
        public long? ContractId { get; set; }


        [Display(Name = "فاکتور خرید")]
        //public BuyFactor BuyFactor { get; set; }
        public long? BuyFactorId { set; get; }

        [Display(Name = "صورت هزینه ")]
        public CostList CostList { get; set; }
        public long? CostListId { set; get; }

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

        public Tafzili Supplier { get; set; }
        public long? SupplierId { get; set; }

       


        public Store Store { get; set; }
        public long? StoreId { get; set; }      

        //public AccDocument AccDocument { get; set; }
        //public AccDocument AccDocument { get; set; }
        public long? AccDocumentId { set; get; }


      

 







        #endregion
    }

}
