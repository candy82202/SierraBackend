namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Specification")]
    public partial class Specification
    {
        public int SpecificationId { get; set; }

        public int? DessertId { get; set; }

        [StringLength(50)]
        public string Flavor { get; set; }

        [StringLength(50)]
        public string Size { get; set; }

        public virtual Dessert Dessert { get; set; }
    }
}
