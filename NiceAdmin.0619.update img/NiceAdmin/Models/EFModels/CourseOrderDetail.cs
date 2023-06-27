namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseOrderDetail
    {
        public int CourseOrderDetailId { get; set; }

        public int CourseOrderId { get; set; }

        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseTitle { get; set; }

        public int NumberOfPeople { get; set; }

        public int CourseUnitPrice { get; set; }

        public int Subtotal { get; set; }

        public virtual CourseOrder CourseOrder { get; set; }

        public virtual Cours Cours { get; set; }
    }
}
