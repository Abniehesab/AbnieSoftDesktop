using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PRO
{
    public class MaterialGroup:BaseEntity
    {

        #region constructor
        [Display(Name = "کد گروه کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? MaterialGroupCode { get; set; }

        [Display(Name = "عنوان گروه کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? MaterialGroupTitle { get; set; }
        #endregion

        #region relation

        public long? BusinessId { set; get; }

        #endregion
    }
}
