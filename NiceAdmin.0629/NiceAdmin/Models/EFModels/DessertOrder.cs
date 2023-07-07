namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DessertOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DessertOrder()
        {
            DessertOrderDetails = new HashSet<DessertOrderDetail>();
        }

        public int DessertOrderId { get; set; }

        public int MemberId { get; set; }

        public int DessertOrderStatusId { get; set; }

        public int? CouponId { get; set; }

        public DateTime CreateTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Recipient { get; set; }

        [Required]
        [StringLength(10)]
        public string RecipientPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipientAddress { get; set; }

        public int ShippingFee { get; set; }

        public int DessertOrderTotal { get; set; }

        [Required]
        [StringLength(50)]
        public string DeliveryMethod { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        [StringLength(200)]
        public string OrderCancellationReason { get; set; }

        [StringLength(200)]
        public string DiscountInfo { get; set; }

        public virtual Coupon Coupon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DessertOrderDetail> DessertOrderDetails { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual Member Member { get; set; }
    }
}
