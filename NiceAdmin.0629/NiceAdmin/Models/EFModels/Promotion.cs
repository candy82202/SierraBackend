namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotion
    {
        public int PromotionId { get; set; }

        public int? CouponId { get; set; }

        [Required]
        [StringLength(255)]
        public string PromotionImage { get; set; }

        [Required]
        [StringLength(50)]
        public string PromotionName { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        public DateTime LaunchAt { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public DateTime CreateAt { get; set; }

        public virtual Coupon Coupon { get; set; }
    }
}
