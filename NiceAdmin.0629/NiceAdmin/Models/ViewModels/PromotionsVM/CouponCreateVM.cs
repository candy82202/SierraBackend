using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class CouponCreateVM
    {
        [Display(Name = "優惠券類別")]
        [Required]
        public int CouponCategoryId { get; set; }
        [Display(Name = "優惠對象")]
        public int? DiscountGroupId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "優惠券名稱")]
        public string CouponName { get; set; }
        [StringLength(50)]
        [Display(Name = "優惠碼")]
        public string CouponCode { get; set; }
        [Display(Name = "折扣限制")]
        [Required]
        public int? LimitType { get; set; }
        [Display(Name = "限制數值")]
        public int? LimitValue { get; set; }
        [Display(Name = "折扣類型")]
        [Required]
        public int DiscountType { get; set; }
        [Display(Name = "折扣數值")]
        public int? DiscountValue { get; set; }
        [Display(Name = "開始時間")]
        public DateTime? StartAt { get; set; }
        [Display(Name = "結束時間")]
        public DateTime? EndAt { get; set; }
        [Display(Name = "使用期限")]
        public int? Expiration { get; set; }
    }
}