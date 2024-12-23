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
    public class ProjectStatusFactor:BaseEntity
    {
        [Display(Name = "کد ضریب صورت وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public int? ProjectStatusFactorCode { get; set; }

        [Display(Name = "عنوان ضریب صورت وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? ProjectStatusFactorTitle { get; set; }

        [Display(Name = "نوع ضریب صورت وضعیت(کاهنده-افزاینده)")]
        public int? ProjectStatusFactorType { get; set; }

        [Display(Name = "کاربر ضریب صورت وضعیت")]
        public int? ProjectStatusFactorUser { get; set; }

        [Display(Name = "درصد ضریب صورت وضعیت")]
        public float? ProjectStatusFactorPercent { get; set; }

        #region relation

        public long? BusinessId { set; get; }

        [Display(Name = "معین")]
        [ForeignKey("Moein")]
        public long? MoeinId { set; get; }
        public Moein Moein { get; set; }

        #endregion
    }
}
