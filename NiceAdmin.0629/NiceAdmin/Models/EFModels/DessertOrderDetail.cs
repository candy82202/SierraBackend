namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DessertOrderDetail
    {
        public int DessertOrderDetailId { get; set; }

        public int DessertOrderId { get; set; }

        public int DessertId { get; set; }

        [Required]
        [StringLength(50)]
        public string DessertName { get; set; }

        public int Quantity { get; set; }

        public int DessertUnitPrice { get; set; }

        public int Subtotal { get; set; }

        public virtual Dessert Dessert { get; set; }

        public virtual DessertOrder DessertOrder { get; set; }
    }
}
