using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class CouponDetailVM
    {
        [Display(Name = "優惠券編號")]
        public int CouponId { get; set; }
        public int CouponCategoryId { get; set; }
        [Display(Name = "優惠券類別")]
        public string CouponCategoryName { get; set; }
        
        public string DiscountGroupName { get; set; }
        [Display(Name = "優惠對象")]
        public string DiscountTarget
        {
            get { return this.DiscountGroupName == null ? "全部商品" : this.DiscountGroupName; }
        }
        [Display(Name = "優惠券名稱")]
        public string CouponName { get; set; }
        [Display(Name = "優惠碼")]
        public string CouponCode { get; set; }
        [Display(Name = "限制類型")]
        public int? LimitType { get; set; }
        [Display(Name = "限制數值")]
        public int? LimitValue { get; set; }
        [Display(Name = "優惠類型")]
        public int DiscountType { get; set; }
        [Display(Name = "優惠數值")]
        public int? DiscountValue { get; set; }
        [Display(Name = "開始時間")]
        public DateTime? StartAt { get; set; }
        [Display(Name = "結束時間")]
        public DateTime? EndAt { get; set; }
        [Display(Name = "有效期限")]
        public int? Expiration { get; set; }
        [Display(Name = "創建時間")]
        public DateTime CreateAt { get; set; }

        public virtual CouponCategory CouponCategory { get; set; }

        public virtual DiscountGroup DiscountGroup { get; set; }
    }
}