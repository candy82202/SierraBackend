namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CouponSetting
    {
        public int CouponSettingId { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponSettingName { get; set; }

        public int? CouponId { get; set; }

        public int CouponType { get; set; }

        public virtual Coupon Coupon { get; set; }
    }
}
