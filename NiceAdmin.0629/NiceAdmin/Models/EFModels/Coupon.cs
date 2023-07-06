namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coupon()
        {
            DessertOrders = new HashSet<DessertOrder>();
            LessonOrders = new HashSet<LessonOrder>();
            MemberCoupons = new HashSet<MemberCoupon>();
            Promotions = new HashSet<Promotion>();
        }

        public int CouponId { get; set; }

        public int CouponCategoryId { get; set; }

        public int? DiscountGroupId { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponName { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponCode { get; set; }

        public int? LimitType { get; set; }

        public int? LimitValue { get; set; }

        public int DiscountType { get; set; }

        public int? DiscountValue { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        public int? Expiration { get; set; }

        public DateTime CreateAt { get; set; }

        public bool Status { get; set; }

        public virtual CouponCategory CouponCategory { get; set; }

        public virtual DiscountGroup DiscountGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DessertOrder> DessertOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonOrder> LessonOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberCoupon> MemberCoupons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
