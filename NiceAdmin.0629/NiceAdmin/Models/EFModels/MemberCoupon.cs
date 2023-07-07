namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MemberCoupon
    {
        public int MemberCouponId { get; set; }

        public int MemberId { get; set; }

        public int CouponId { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UseAt { get; set; }

        public DateTime ExpireAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponName { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Member Member { get; set; }
    }
}
