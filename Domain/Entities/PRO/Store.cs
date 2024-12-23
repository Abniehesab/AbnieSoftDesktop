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
    public class Store : BaseEntity
    {

        #region constructor

        [Display(Name = "کد انبار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? StoreCode { get; set; }

        [Display(Name = "عنوان انبار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? StoreTitle { get; set; }

        [Display(Name = "مسئول انبار")]
        
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? StoreAdmin { get; set; }

        [Display(Name = "آدرس انبار")]
   
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? StoreAddress { get; set; }

        #endregion


        #region relation

        public long? BusinessId { set; get; }

        [Display(Name = "معین")]
        [ForeignKey("Moein")]
        public long? MoeinId { set; get; }
        public Moein Moein { get; set; }

        #endregion
    }
}
