namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dessert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dessert()
        {
            DessertImages = new HashSet<DessertImage>();
            DessertOrderDetails = new HashSet<DessertOrderDetail>();
            DessertTags = new HashSet<DessertTag>();
            DiscountGroupItems = new HashSet<DiscountGroupItem>();
            Specifications = new HashSet<Specification>();
        }

        public int DessertId { get; set; }

        [Required]
        [StringLength(50)]
        public string DessertName { get; set; }

        public int CategoryId { get; set; }

        public int UnitPrice { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DessertImage> DessertImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DessertOrderDetail> DessertOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DessertTag> DessertTags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountGroupItem> DiscountGroupItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Specification> Specifications { get; set; }
    }
}
