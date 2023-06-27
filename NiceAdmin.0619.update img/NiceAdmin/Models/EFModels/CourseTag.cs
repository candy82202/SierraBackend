namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseTag
    {
        public int CourseTagId { get; set; }

        public int CourseId { get; set; }

        [Column("CourseTag")]
        [Required]
        [StringLength(20)]
        public string CourseTag1 { get; set; }

        public virtual Cours Cours { get; set; }
    }
}
