namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseImage
    {
        public int CourseImageId { get; set; }

        public int CourseId { get; set; }

        [Column("CourseImage")]
        [Required]
        [StringLength(255)]
        public string CourseImage1 { get; set; }

        public virtual Cours Cours { get; set; }
    }
}
