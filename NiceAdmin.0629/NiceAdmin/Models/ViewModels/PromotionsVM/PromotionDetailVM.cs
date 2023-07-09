using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class PromotionDetailVM
    {
        public int PromotionId { get; set; }
        public string PromotionImage { get; set; }
        public string CouponName { get; set; }
        public string PromotionName { get; set; }
        public string Description { get; set; }
        public DateTime LaunchAt { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}