namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DiscountGroupItem
    {
        public int DiscountGroupItemId { get; set; }

        public int DiscountGroupId { get; set; }

        public int DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }

        public virtual DiscountGroup DiscountGroup { get; set; }
    }
}
