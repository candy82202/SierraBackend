namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LessonImage
    {
        public int LessonImageId { get; set; }

        public int LessonId { get; set; }

        [Required]
        [StringLength(255)]
        public string LessonImageName { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
