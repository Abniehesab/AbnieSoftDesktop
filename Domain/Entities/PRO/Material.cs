
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
    public class Material:BaseEntity
    {
        #region constructor
        [Display(Name = "نوع کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? MaterialType { get; set; }

        [Display(Name = "کد کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? MaterialCode { get; set; }

        [Display(Name = "عنوان کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? MaterialTitle { get; set; }

        [Display(Name = "توضیحات کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? MaterialDescription { get; set; }

        #endregion

        #region relation

        [Display(Name = "کد واحد کالا")]
        [ForeignKey("MaterialUnit")]
        public long? MaterialUnitId { get; set; }
        public MaterialUnit MaterialUnit { get; set; }
       


        [Display(Name = "کد گروه کالا")]
        [ForeignKey("MaterialGroup")]
        public long? MaterialGroupId { get; set; }
        public MaterialGroup MaterialGroup { get; set; }



        [Display(Name = "کسب و کار")]
        //public Business Business { get; set; }
        public long? BusinessId { set; get; }




        #endregion
    }
}
