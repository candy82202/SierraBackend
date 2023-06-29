namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LessonOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonOrder()
        {
            LessonOrderDetails = new HashSet<LessonOrderDetail>();
        }

        public int LessonOrderId { get; set; }

        public int MemberId { get; set; }

        public int LessonOrderStatusId { get; set; }

        public int? CouponId { get; set; }

        public DateTime CreateTime { get; set; }

        public int LessonOrderTotal { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        [StringLength(200)]
        public string OrderCancellationReason { get; set; }

        [StringLength(200)]
        public string DiscountInfo { get; set; }

        public virtual Coupon Coupon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonOrderDetail> LessonOrderDetails { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual Member Member { get; set; }
    }
}
