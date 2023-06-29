namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Discount
    {
        public int DiscountId { get; set; }

        public int? DiscountGroupId { get; set; }

        [Required]
        [StringLength(50)]
        public string DiscountName { get; set; }

        public int? LimitType { get; set; }

        public int? LimitValue { get; set; }

        public int DiscountType { get; set; }

        public int? DiscountValue { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public DateTime CreateAt { get; set; }

        public virtual DiscountGroup DiscountGroup { get; set; }
    }
}
