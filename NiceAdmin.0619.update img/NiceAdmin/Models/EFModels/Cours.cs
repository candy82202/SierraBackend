namespace NiceAdmin.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            CourseImages = new HashSet<CourseImage>();
            CourseOrderDetails = new HashSet<CourseOrderDetail>();
            CourseTags = new HashSet<CourseTag>();
        }

        [Key]
        public int CourseId { get; set; }

        public int CourseCategoryId { get; set; }

        public int TeacherId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseTitle { get; set; }

        [Required]
        [StringLength(150)]
        public string CourseInfo { get; set; }

        [Required]
        [StringLength(300)]
        public string CourseDetail { get; set; }

        [Required]
        [StringLength(20)]
        public string CourseDessert { get; set; }

        public DateTime CourseTime { get; set; }

        public int CourseHours { get; set; }

        public int MaximumCapacity { get; set; }

        public int CoursePrice { get; set; }

        public bool CourseStatus { get; set; }

        public DateTime CreateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseImage> CourseImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseOrderDetail> CourseOrderDetails { get; set; }

        public virtual CoursesCategory CoursesCategory { get; set; }

        public virtual Teacher Teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseTag> CourseTags { get; set; }
    }
}
