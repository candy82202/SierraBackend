namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DessertImage
    {
        [Key]
        public int ImageId { get; set; }

        public int? DessertId { get; set; }

        [StringLength(255)]
        public string DessertImageName { get; set; }

        public virtual Dessert Dessert { get; set; }
    }
}
