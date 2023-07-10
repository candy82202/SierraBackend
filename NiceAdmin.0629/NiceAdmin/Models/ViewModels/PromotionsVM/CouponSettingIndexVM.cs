using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class CouponSettingIndexVM
    {
        public int CouponSettingId { get; set; }
        public string CouponSettingName { get; set; }
        public int? CouponId { get; set; }
        public int CouponType { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}