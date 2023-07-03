using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class PromotionIndexVM
    {
        [Display(Name ="圖片")]
        public string PromotionImage { get; set; }
        public int PromotionId { get; set; }
        [Display(Name = "活動名稱")]
        public string PromotionName { get; set; }
        [Display(Name = "相關優惠券")]
        public string CouponName { get; set; }
        [Display(Name = "活動詳情")]
        public string Description { get; set; }
        [Display(Name = "發佈時間")]
        public DateTime LaunchAt { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }
        [Display(Name = "是否上架")]
        public string Status
        {
            get { if (LaunchAt > DateTime.Now) return "未上架";
                else if (StartAt < DateTime.Now && EndAt > DateTime.Now) return "上架中";
                else return "已下架";
            }
        }
        [Display(Name = "創建時間")]
        public DateTime CreateAt { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}