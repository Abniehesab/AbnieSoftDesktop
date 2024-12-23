using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.ACC
{
    public class TafziliType:BaseEntity
    {
        #region constructor
        

        [Display(Name = "کد نوع تفضیلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? TafziliTypeCode { get; set; }
        [Display(Name = "نام نوع تفضیلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? TafziliTypeName { get; set; }
        #endregion

        #region relation
        public ICollection<TafziliGroup> TafziliGroups { get; set; }
        #endregion

    }
}
