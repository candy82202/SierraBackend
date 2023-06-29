namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DessertTag
    {
        public int DessertTagId { get; set; }

        public int? DessertId { get; set; }

        [Required]
        [StringLength(50)]
        public string DessertTagName { get; set; }

        public virtual Dessert Dessert { get; set; }
    }
}
