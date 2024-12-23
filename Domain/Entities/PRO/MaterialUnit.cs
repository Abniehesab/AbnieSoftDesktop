using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PRO
{
    public class MaterialUnit:BaseEntity
    {
        #region constructor
        [Display(Name = "کد واحد کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? MaterialUnitCode { get; set; }

        [Display(Name = "نام واحد کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? MaterialUnitTitle { get; set; }
        #endregion

        #region relation

        public long? BusinessId { set; get; }

        #endregion
    }
}
