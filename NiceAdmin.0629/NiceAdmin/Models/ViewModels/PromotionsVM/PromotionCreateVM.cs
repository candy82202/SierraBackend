using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class PromotionCreateVM
    {
        [Display(Name = "相關優惠券")]
        public int? CouponId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "圖片")]
        public string PromotionImage { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "活動名稱")]
        public string PromotionName { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "活動詳情")]
        public string Description { get; set; }
        [Display(Name = "發布時間")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime LaunchAt { get; set; }
        [Display(Name = "開始時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StartAt { get; set; }
        [Display(Name = "結束時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime EndAt { get; set; }
    }
}