namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lesson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lesson()
        {
            LessonImages = new HashSet<LessonImage>();
            LessonOrderDetails = new HashSet<LessonOrderDetail>();
            LessonTags = new HashSet<LessonTag>();
        }

        public int LessonId { get; set; }

        public int LessonCategoryId { get; set; }

        public int TeacherId { get; set; }

        [Required]
        [StringLength(50)]
        public string LessonTitle { get; set; }

        [Required]
        [StringLength(150)]
        public string LessonInfo { get; set; }

        [Required]
        [StringLength(300)]
        public string LessonDetail { get; set; }

        [Required]
        [StringLength(20)]
        public string LessonDessert { get; set; }

        public DateTime LessonTime { get; set; }

        public int LessonHours { get; set; }

        public int MaximumCapacity { get; set; }

        public int LessonPrice { get; set; }

        public bool LessonStatus { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual LessonCategory LessonCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonImage> LessonImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonOrderDetail> LessonOrderDetails { get; set; }

        public virtual Teacher Teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonTag> LessonTags { get; set; }
    }
}
