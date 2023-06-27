namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseOrder()
        {
            CourseOrderDetails = new HashSet<CourseOrderDetail>();
        }

        public int CourseOrderId { get; set; }

        public int MemberId { get; set; }

        public int CourseOrderStatusId { get; set; }

        public int CouponId { get; set; }

        public DateTime CreateTime { get; set; }

        public int CourseOrderTotal { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        [StringLength(200)]
        public string OrderCancellationReason { get; set; }

        public virtual Coupon Coupon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseOrderDetail> CourseOrderDetails { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        public virtual Member Member { get; set; }
    }
}
