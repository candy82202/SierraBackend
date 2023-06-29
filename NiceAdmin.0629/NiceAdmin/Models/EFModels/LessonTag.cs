namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LessonTag
    {
        public int LessonTagId { get; set; }

        public int LessonId { get; set; }

        [Required]
        [StringLength(20)]
        public string LessonTagName { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
