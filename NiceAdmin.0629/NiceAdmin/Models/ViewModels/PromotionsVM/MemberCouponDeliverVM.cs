using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.PromotionsVM
{
    public class MemberCouponDeliverVM
    {
        public int MemberId { get; set; }
        public int CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Member Member { get; set; }
    }
}